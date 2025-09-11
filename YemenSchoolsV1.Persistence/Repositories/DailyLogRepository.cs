using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    internal class DailyLogRepository : IDailyLogRepository
    {
        private readonly YemenShoolsDbContext _context;

        public DailyLogRepository(YemenShoolsDbContext context)
        {
            _context = context;
        }
        // AppRepository.cs

        public async Task<IEnumerable<DailyLog>> GetStudentDailyLogsForDayAsync(Guid studentId, DateTime date)
        {
            var student = await _context.Students
                                        .Include(s => s.CurrentSection)
                                        .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null || student.CurrentSectionId == Guid.Empty)
            {
                return Enumerable.Empty<DailyLog>();
            }

            // قم بتصفية السجلات حسب تاريخ اليوم المحدد
            var logs = await _context.DailyLogs
                                     .Include(dl => dl.SectionSubject)
                                     .ThenInclude(ss => ss.GradeSubject)
                                     .ThenInclude(gs => gs.Subject)
                                     .Where(dl => dl.SectionSubject.SectionId == student.CurrentSectionId &&
                                                  dl.Date.Date == date.Date)
                                     .OrderByDescending(dl => dl.Date)
                                     .ToListAsync();

            return logs;
        }

        // ------------------- إضافة سجل يومي -------------------
        public async Task AddDailyLogAsync(DailyLog dailyLog)
        {
            dailyLog.Date = DateTime.UtcNow;
            await _context.DailyLogs.AddAsync(dailyLog);
            await _context.SaveChangesAsync();
        }

        // ------------------- جلب سجل يومي بواسطة ID -------------------
        public async Task<DailyLog?> GetDailyLogByIdAsync(Guid id)
        {
            return await _context.DailyLogs
                                 .Include(dl => dl.SectionSubject)
                                 .ThenInclude(ss => ss.Section)
                                 .FirstOrDefaultAsync(dl => dl.Id == id);
        }

        // ------------------- جلب سجلات طالب معين -------------------
        public async Task<IEnumerable<DailyLog>> GetStudentDailyLogsAsync(Guid studentId)
        {
            // استعلام معقد للوصول إلى سجلات الطالب
            var student = await _context.Students
                                        .Include(s => s.CurrentSection)
                                        .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null || student.CurrentSectionId == Guid.Empty)
            {
                return Enumerable.Empty<DailyLog>();
            }

            // استخدام معرف الشعبة للوصول إلى السجلات اليومية
            return await _context.DailyLogs
                                 .Include(dl => dl.SectionSubject)
                                 .ThenInclude(ss => ss.GradeSubject)
                                 .ThenInclude(gs => gs.Subject) // لربط الموضوع بالاسم
                                 .Where(dl => dl.SectionSubject.SectionId == student.CurrentSectionId)
                                 .OrderByDescending(dl => dl.Date)
                                 .ToListAsync();
        }
    }
}
