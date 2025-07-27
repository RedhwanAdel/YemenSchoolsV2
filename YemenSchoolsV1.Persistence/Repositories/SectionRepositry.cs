using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
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

        public async Task<IEnumerable<Section>> GetSectionsByAcademicYearAndSchoolGradeAsync(Guid academicYearId, Guid schoolGradeId)
        {
            return await dbContext.Sections
                                 .Where(cs => cs.AcademicYearId == academicYearId && cs.SchoolGradeId == schoolGradeId)
                                 .Include(cs => cs.AcademicYear)
                                 .Include(cs => cs.SchoolGrade)
                                 .ToListAsync();
        }

        public async Task<Section?> GetSectionByIdAsync(Guid sectionId)
        {
            return await dbContext.Sections
                                 .Include(cs => cs.AcademicYear)
                                 .Include(cs => cs.SchoolGrade)
                                 .FirstOrDefaultAsync(cs => cs.Id == sectionId);
        }

        public async Task<List<SectionSummaryDto>> GetSectionSummariesByAcademicYearAsync(Guid academicYearId)
        {
            return await dbContext.Sections
                .Where(s => s.AcademicYearId == academicYearId)
                .Include(s => s.SchoolGrade)
                    .ThenInclude(sg => sg.StageGrade)
                        .ThenInclude(stg => stg.Grade)
                .Include(s => s.SchoolGrade)
                    .ThenInclude(sg => sg.GradeSubjects)
                .Select(s => new SectionSummaryDto
                {
                    SectionId = s.Id,
                    SectionName = s.Name,
                    GradeName = s.SchoolGrade.StageGrade.Grade.Name,
                    SubjectCount = s.SchoolGrade.GradeSubjects.Count
                })
                .ToListAsync();
        }
    }
}
