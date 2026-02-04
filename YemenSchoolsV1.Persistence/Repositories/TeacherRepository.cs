using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class TeacherRepository : GenericRepositoryAsync<Teacher>, ITeacherRepository
    {
        private readonly YemenShoolsDbContext dbContext;

        public TeacherRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<SectionSubject>> GetTeacherSectionSubjectsAsync(Guid teacherId)
        {
            return await dbContext.SectionSubjects
                .Where(ss => ss.TeacherId == teacherId)
                .Include(ss => ss.Section)
                .Include(ss => ss.GradeSubject)
                    .ThenInclude(gs => gs.Subject)
                    .Include(s => s.Section)
                    .ThenInclude(s => s.SchoolGrade)
                    .ThenInclude(s => s.StageGrade)
                    .ThenInclude(s => s.Grade)
                .ToListAsync();
        }
    }
}
