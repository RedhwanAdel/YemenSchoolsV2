using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IAttendanceRepository : IGenericRepositoryAsync<Attendance>
    {
        Task<Attendance> CreateAttendanceAsync(Attendance attendance);
        Task<List<AttendanceDetail>> CreateAttendanceDetailsAsync(List<AttendanceDetail> details);
        Task<Attendance?> GetAttendanceByIdAsync(Guid attendanceId);
        Task<Attendance?> GetAttendanceByDateAndSectionAsync(DateTime date, Guid sectionId);
        Task UpdateAttendanceDetailsAsync(List<AttendanceDetail> details);
        IQueryable<Attendance> GetAll();

    }
}
