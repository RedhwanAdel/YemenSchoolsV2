using Microsoft.EntityFrameworkCore;
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

        public async Task<Grade?> GetGradeByIdIncludeAsync(Guid id)
        {
            return await dbContext.Grades.Where(e => e.Id == id).Include(r => r.Term).SingleOrDefaultAsync();
        }
    }
}
