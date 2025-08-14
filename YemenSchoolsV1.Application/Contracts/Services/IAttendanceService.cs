using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IAttendanceService
    {
        Task<Attendance> CreateDailyAttendanceAsync(Guid classTeacherId, Guid sectionId, DateTime date, Dictionary<Guid, AttendanceStatus> studentStatuses);
        Task UpdateDailyAttendanceAsync(Guid attendanceId, Dictionary<Guid, AttendanceStatus> newStudentStatuses);
        Task<List<AttendanceDetail>> GetStudentAttendanceReportAsync(Guid studentId);
    }
}
