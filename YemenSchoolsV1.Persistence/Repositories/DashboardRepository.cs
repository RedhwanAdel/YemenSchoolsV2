namespace YemenSchoolsV1.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using YemenSchoolsV1.Application.Contracts.Persistence;
    using YemenSchoolsV1.Persistence.Data;

    public class DashboardRepository : IDashboardRepository
    {
        private readonly YemenShoolsDbContext _context;

        public DashboardRepository(YemenShoolsDbContext context)
        {
            _context = context;
        }


        public async Task<DashboardDto> GetDashboardAsync()
        {
            // 1️⃣ Summary
            var summary = new DashboardSummaryDto
            {
                TotalCities = await _context.Cities.CountAsync(),
                TotalRegions = await _context.Regions.CountAsync(),
                TotalSchools = await _context.Schools.CountAsync(),
                ActiveSchools = await _context.Schools.CountAsync(s => s.IsActive),
                TotalTeachers = await _context.Teachers.CountAsync(),
                TotalStudents = await _context.Students.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                CurrentAcademicYear = await _context.AcademicYears
                    .Where(y => y.IsCurrentYear)
                    .Select(y => y.Name)
                    .FirstOrDefaultAsync()
            };

            // 2️⃣ Top Schools by Students
            var topSchoolsByStudents = await _context.Students
                .GroupBy(s => s.SchoolId)
                .Select(g => new TopSchoolDto
                {
                    SchoolName = g.First().School.NameAr,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            // 3️⃣ Top Schools by Teachers
            var topSchoolsByTeachers = await _context.Teachers
                .GroupBy(t => t.SchoolId)
                .Select(g => new TopSchoolDto
                {
                    SchoolName = g.First().School.NameAr,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            // 4️⃣ Student Growth Last 6 Months (بيانات تجريبية للعرض)
            var months = new[] { "May", "Jun", "Jul", "Aug", "Sep", "Oct" };
            var studentGrowth = months.Select((m, i) => new StudentGrowthDto
            {
                Month = m,
                Students = 200 + (i * 100) // مجرد أرقام تجريبية للعرض
            }).ToList();

            // 5️⃣ Recent Activities
            var recentActivities = new List<RecentActivityDto>();

            // أحدث 3 مدارس (حسب Id)
            var latestSchools = await _context.Schools
                .OrderByDescending(s => s.Id)
                .Take(3)
                .Select(s => new RecentActivityDto
                {
                    Type = "School",
                    Name = s.NameAr,
                    Action = "Added",
                    Date = DateTime.Now // للعرض فقط
                })
                .ToListAsync();

            // أحدث 3 معلمين (حسب Id)
            var latestTeachers = await _context.Teachers
                .OrderByDescending(t => t.Id)
                .Take(3)
                .Select(t => new RecentActivityDto
                {
                    Type = "Teacher",
                    Name = t.NameAr,
                    Action = "Joined",
                    Date = DateTime.Now // للعرض فقط
                })
                .ToListAsync();

            // أحدث 3 أخبار مدارس (لو يوجد CreatedDate أو Date)
            var latestNews = await _context.SchoolNews
                .OrderByDescending(n => n.CreatedDate) // إذا CreatedDate موجود
                .Take(3)
                .Select(n => new RecentActivityDto
                {
                    Type = "News",
                    Name = n.Title,
                    Action = "Published",
                    Date = n.CreatedDate
                })
                .ToListAsync();

            recentActivities.AddRange(latestSchools);
            recentActivities.AddRange(latestTeachers);
            recentActivities.AddRange(latestNews);

            // 6️⃣ تجميع كل البيانات
            return new DashboardDto
            {
                Summary = summary,
                TopSchoolsByStudents = topSchoolsByStudents,
                TopSchoolsByTeachers = topSchoolsByTeachers,
                StudentGrowthLast6Months = studentGrowth,
                RecentActivities = recentActivities
            };
        }
    }

}
