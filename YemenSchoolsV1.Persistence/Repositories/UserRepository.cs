using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class UserRepository : GenericRepositoryAsync<AppUser>, IUserRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public UserRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
