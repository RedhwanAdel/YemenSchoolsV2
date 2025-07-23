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


    }
}
