using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class TeacherRepositry : GenericRepositoryAsync<Teacher>, ITeacherRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public TeacherRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<SectionSubject>> GetTeacherSectionSubjectsAsync(Guid teacherId)
        {
            return await dbContext.SectionSubject
                .Where(ss => ss.TeacherId == teacherId)
                .Include(ss => ss.Section)
                .Include(ss => ss.GradeSubject)
                    .ThenInclude(gs => gs.Subject)
                .ToListAsync();
        }
    }
}
