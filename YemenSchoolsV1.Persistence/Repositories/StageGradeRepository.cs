using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class StageGradeRepository : GenericRepositoryAsync<StageGrade>, IStageGradeRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public StageGradeRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<StageGrade>> GetAllStageGradesAsync()
        {
            return await dbContext.StageGrades
                .Include(sg => sg.Stage)
                .Include(sg => sg.Grade)
                .ToListAsync();
        }
    }
}
