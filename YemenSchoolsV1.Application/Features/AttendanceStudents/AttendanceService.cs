using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.AttendanceStudents
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ISectionRepositry _sectionRepository; // افتراض وجود مستودع للشعبة

        public AttendanceService(IAttendanceRepository attendanceRepository, ISectionRepositry sectionRepository)
        {
            _attendanceRepository = attendanceRepository;
            _sectionRepository = sectionRepository;
        }

        // الدالة الأولى: لإنشاء سجل حضور يومي
        public async Task<Attendance> CreateDailyAttendanceAsync(Guid classTeacherId, Guid sectionId, DateTime date, Dictionary<Guid, AttendanceStatus> studentStatuses)
        {
            // 1. التحقق من وجود سجل حضور مسبقًا لهذا اليوم وللشعبة
            var existingAttendance = await _attendanceRepository.GetAttendanceByDateAndSectionAsync(date, sectionId);
            if (existingAttendance != null)
            {
                throw new InvalidOperationException("Attendance record for this section and date already exists.");
            }

            // 2. التحقق من صلاحية المعلم (هل هو مربي صف هذه الشعبة؟)
            var section = await _sectionRepository.GetSectionByIdAsync(sectionId);
            if (section == null || section.ClassTeacherId != classTeacherId)
            {
                throw new UnauthorizedAccessException("Teacher is not authorized to take attendance for this section.");
            }

            // 3. إنشاء سجل الحضور الرئيسي
            var attendance = new Attendance
            {
                Date = date,
                SectionId = sectionId,
                ClassTeacherId = classTeacherId,
                AcademicYearId = section.AcademicYearId // يمكن جلبها من الشعبة
            };

            // 4. إنشاء تفاصيل الحضور بناءً على حالات الطلاب
            var attendanceDetails = studentStatuses.Select(s => new AttendanceDetail
            {
                StudentId = s.Key,
                Status = s.Value
            }).ToList();

            attendance.AttendanceDetails = attendanceDetails;

            // 5. حفظ البيانات في قاعدة البيانات
            return await _attendanceRepository.CreateAttendanceAsync(attendance);
        }

        // الدالة الثانية: لتحديث سجل حضور قائم
        public async Task UpdateDailyAttendanceAsync(Guid attendanceId, Dictionary<Guid, AttendanceStatus> newStudentStatuses)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(attendanceId);
            if (attendance == null)
            {
                throw new KeyNotFoundException("Attendance record not found.");
            }

            // تحديث الحالات لكل طالب
            foreach (var detail in attendance.AttendanceDetails)
            {
                if (newStudentStatuses.TryGetValue(detail.StudentId, out var newStatus))
                {
                    detail.Status = newStatus;
                }
            }

            await _attendanceRepository.UpdateAttendanceDetailsAsync(attendance.AttendanceDetails.ToList());
        }

        // الدالة الثالثة: لجلب تقرير حضور طالب
        public async Task<List<AttendanceDetail>> GetStudentAttendanceReportAsync(Guid studentId)
        {
            return await _attendanceRepository.GetAll()
                                              .Include(a => a.AttendanceDetails) // لتحميل البيانات المرتبطة
                                              .Where(a => a.AttendanceDetails.Any(ad => ad.StudentId == studentId))
                                              .SelectMany(a => a.AttendanceDetails)
                                              .Where(ad => ad.StudentId == studentId)
                                              .ToListAsync();
        }
    }
}
