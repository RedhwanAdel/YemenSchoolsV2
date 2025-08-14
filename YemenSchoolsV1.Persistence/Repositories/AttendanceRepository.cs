using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class AttendanceRepository : GenericRepositoryAsync<Attendance>, IAttendanceRepository
    {
        private readonly YemenShoolsDbContext _context;

        public AttendanceRepository(YemenShoolsDbContext context) : base(context)
        {
            _context = context;
        }

        // الدالة الأولى: لإنشاء سجل حضور جديد لجلسة معينة
        public async Task<Attendance> CreateAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        // الدالة الثانية: لإنشاء تفاصيل الحضور (حالات الطلاب)
        public async Task<List<AttendanceDetail>> CreateAttendanceDetailsAsync(List<AttendanceDetail> details)
        {
            await _context.AttendanceDetails.AddRangeAsync(details);
            await _context.SaveChangesAsync();
            return details;
        }

        // الدالة الثالثة: لجلب سجل حضور معين مع تفاصيله
        public async Task<Attendance?> GetAttendanceByIdAsync(Guid attendanceId)
        {
            return await _context.Attendances
                                 .Include(a => a.AttendanceDetails)
                                 .FirstOrDefaultAsync(a => a.Id == attendanceId);
        }

        // الدالة الرابعة: لجلب سجل حضور ليوم وشعبة معينة
        public async Task<Attendance?> GetAttendanceByDateAndSectionAsync(DateTime date, Guid sectionId)
        {
            return await _context.Attendances
                                 .Include(a => a.AttendanceDetails)
                                 .FirstOrDefaultAsync(a => a.Date.Date == date.Date && a.SectionId == sectionId);
        }

        // الدالة الخامسة: لتحديث تفاصيل الحضور (مثلاً، تغيير حالة طالب)
        public async Task UpdateAttendanceDetailsAsync(List<AttendanceDetail> details)
        {
            _context.AttendanceDetails.UpdateRange(details);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Attendance> GetAll()
        {
            return _context.Attendances;
        }
    }
}
