using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SectionSubjectRepository : GenericRepositoryAsync<SectionSubject>, ISectionSubjectRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public SectionSubjectRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<SectionSubject?> GetSectionSubjectsInfoAsync(Guid Id)
        {
            return await dbContext.SectionSubjects
                .Include(ss => ss.Section)
                .Include(ss => ss.GradeSubject)
                    .ThenInclude(gs => gs.Subject)
                    .FirstOrDefaultAsync(ss => ss.Id == Id);


        }

        public async Task<List<SectionSubjectInfoDto>> GetSectionSubjectsInfoBySectionIdAsync(Guid sectionId)
        {
            return await dbContext.SectionSubjects
                .Where(ss => ss.SectionId == sectionId)
                .Include(ss => ss.GradeSubject)
                    .ThenInclude(gs => gs.Subject)

                .Include(ss => ss.Term)
                .Include(ss => ss.Teacher)
                .Select(ss => new SectionSubjectInfoDto
                {
                    Id = ss.Id,
                    SectionId = ss.SectionId,
                    GradeSubjectId = ss.GradeSubjectId,
                    TermId = ss.TermId,
                    TeacherId = ss.TeacherId,
                    SubjectId = ss.GradeSubject.Subject.Id,
                    SubjectName = ss.GradeSubject.Subject.Name,
                    TermName = ss.Term.Name,
                    TeacherName = ss.Teacher != null ? ss.Teacher.NameAr : null
                })
                .ToListAsync();
        }
    }
}
