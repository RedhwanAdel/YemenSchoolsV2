using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    internal class TermRepositry : GenericRepositoryAsync<Term>, ITermRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public TermRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Term?> GetTermByIdIncludeAsync(Guid id)
        {
            return await dbContext.Terms.Where(e => e.Id == id).Include(r => r.AcademicYear).SingleOrDefaultAsync();
        }
    }
}
