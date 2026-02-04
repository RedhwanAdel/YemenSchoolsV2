using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    internal class TermRepository : GenericRepositoryAsync<Term>, ITermRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public TermRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Term>> GetTermByYearIdAsync(Guid id)
        {
            return await dbContext.Terms.Include(x => x.AcademicYear).Where(e => e.AcademicYearId == id).ToListAsync();
        }
        public async Task<Term?> GetTermByIdIncludeAsync(Guid id)
        {
            return await dbContext.Terms.Where(e => e.Id == id).Include(r => r.AcademicYear).SingleOrDefaultAsync();
        }
    }
}
