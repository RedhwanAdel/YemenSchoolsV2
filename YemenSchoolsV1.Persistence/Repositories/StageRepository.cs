using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class StageRepository : GenericRepositoryAsync<Stage>, IStageRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public StageRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
