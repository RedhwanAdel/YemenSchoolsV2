using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class GradeRepositry : GenericRepositoryAsync<Grade>, IGradeRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public GradeRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
