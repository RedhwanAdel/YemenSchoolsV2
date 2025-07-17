using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SectionRepositry : GenericRepositoryAsync<Section>, ISectionRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public SectionRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Section?> GetSectioneByIdIncludeAsync(Guid id)
        {
            return await dbContext.Sections.Where(e => e.Id == id).Include(r => r.Grade).SingleOrDefaultAsync();
        }
    }
}
