using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.API.Dto;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SchoolGradeRepository : GenericRepositoryAsync<SchoolGrade>, ISchoolGradeRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public SchoolGradeRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SyncSchoolStageGradesAsync(Guid schoolId, List<Guid> stageGradeIds)
        {
            if (stageGradeIds == null || stageGradeIds.Count == 0)
                return;

            var existingGrades = await dbContext.SchoolGrade
                .AsNoTracking()
                .Where(sg => sg.SchoolId == schoolId)
                .ToListAsync();

            var newIds = stageGradeIds.ToHashSet();
            var existingIds = existingGrades.Select(e => e.StageGradeId).ToHashSet();

            var toAdd = newIds.Except(existingIds)
                .Select(id => new SchoolGrade
                {
                    SchoolId = schoolId,
                    StageGradeId = id
                })
                .ToList();

            var toRemove = existingGrades
                .Where(e => !newIds.Contains(e.StageGradeId))
                .ToList();

            var hasChanges = false;

            if (toRemove.Count > 0)
            {
                dbContext.SchoolGrade.RemoveRange(toRemove);
                hasChanges = true;
            }
            if (toAdd.Count > 0)
            {
                await dbContext.SchoolGrade.AddRangeAsync(toAdd);
                hasChanges = true;
            }
            if (hasChanges)
                await dbContext.SaveChangesAsync();

        }


        public async Task<List<StageGradeDto>> GetStageGradesAsync(Guid schoolId)
        {
            var result = await dbContext.StageGrade
                .Select(sg => new StageGradeDto
                {
                    StageGradeId = sg.Id,
                    StageName = sg.Stage != null ? sg.Stage.Name : string.Empty,
                    GradeName = sg.Grade != null ? sg.Grade.Name : string.Empty,
                    IsSelected = dbContext.SchoolGrade
                        .Any(schoolGrade => schoolGrade.SchoolId == schoolId && schoolGrade.StageGradeId == sg.Id)
                })
                .ToListAsync();

            return result;
        }


        public async Task<List<SchoolGradeDto>> GetSchoolGradesBySchoolIdAsync(Guid schoolId)
        {
            var result = await dbContext.SchoolGrade
                .Include(x => x.StageGrade)
                .Select(sg => new SchoolGradeDto
                {
                    Id = sg.Id,
                    StageGradeId = sg.StageGradeId,
                    SchoolId = sg.SchoolId,
                    StageName = sg.StageGrade != null ? sg.StageGrade.Stage.Name : string.Empty,
                    GradeName = sg.StageGrade != null ? sg.StageGrade.Grade.Name : string.Empty
                }).Where(x => x.SchoolId == schoolId)
                .ToListAsync();

            return result;
        }


    }
}
