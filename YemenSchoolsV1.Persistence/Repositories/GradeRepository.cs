using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class GradeRepository : GenericRepositoryAsync<Grade>, IGradeRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public GradeRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
