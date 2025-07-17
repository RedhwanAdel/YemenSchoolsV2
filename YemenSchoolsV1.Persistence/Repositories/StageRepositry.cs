using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class StageRepositry : GenericRepositoryAsync<Stage>, IStageRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public StageRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Stage?> GetStageByIdIncludeAsync(Guid id)
        {
            return await dbContext.Stages.Where(e => e.Id == id).Include(r => r.School).SingleOrDefaultAsync();
        }
    }
}
