using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IDailyLogRepository
    {
        Task<IEnumerable<DailyLog>> GetStudentDailyLogsForDayAsync(Guid studentId, DateTime date);

        Task<DailyLog?> GetDailyLogByIdAsync(Guid id);
        Task AddDailyLogAsync(DailyLog dailyLog);
        Task<IEnumerable<DailyLog>> GetStudentDailyLogsAsync(Guid studentId);
    }
}
