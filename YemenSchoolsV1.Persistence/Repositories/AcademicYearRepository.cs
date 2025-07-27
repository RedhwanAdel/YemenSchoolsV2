using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class AcademicYearRepository : GenericRepositoryAsync<AcademicYear>, IAcademicYearRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public AcademicYearRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<AcademicYear>> GetYearsBySchoolIdAsync(Guid id)
        {
            return await dbContext.AcademicYears.Where(e => e.SchoolId == id).ToListAsync();
        }
        public async Task<Guid?> SetCurrentYearAsync(Guid schoolId, Guid academicYearId)
        {
            var years = await dbContext.AcademicYears
                .Where(y => y.SchoolId == schoolId)
                .ToListAsync();


            if (years.Count == 0)
                return null;

            foreach (var year in years)
                year.IsCurrentYear = false;

            var currentYear = years.FirstOrDefault(y => y.Id == academicYearId);
            if (currentYear == null)
                return null;

            currentYear.IsCurrentYear = true;

            await dbContext.SaveChangesAsync();
            return currentYear.Id;
        }

        public async Task<Guid?> GetCurrentYearIdAsync(Guid schoolId)
        {
            return await dbContext.AcademicYears
                .Where(y => y.SchoolId == schoolId && y.IsCurrentYear)
                .Select(y => (Guid?)y.Id)
                .FirstOrDefaultAsync();
        }

    }
}
