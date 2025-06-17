using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
	public class AssignedSubjectRepository : GenericRepositoryAsync<AssignedSubject>, IAssignedSubjectRepository
	{
		private readonly YemenShoolsDbContext dbContext;

		public AssignedSubjectRepository(YemenShoolsDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}


		// Returns a list of AssignedSubjects for a specific teacher,
		// including the teacher, subject grade, and section.
		public async Task<List<AssignedSubject>> GetByTeacherIdAsync(Guid teacherId)
		{
			return await dbContext.AssignedSubjects
				.Include(a => a.Teacher)
				.Include(a => a.Subject)
				.Include(a => a.Section)
				.Where(a => a.TeacherId == teacherId)
				.ToListAsync();
		}

		// Returns a list of AssignedSubjects for a specific section,
		// including the teacher, subject grade, and section details.
		public async Task<List<AssignedSubject>> GetBySectionIdAsync(Guid sectionId)
		{
			return await dbContext.AssignedSubjects
				.Include(a => a.Teacher)
				.Include(a => a.Subject)
				.Include(a => a.Section)
				.Where(a => a.SectionId == sectionId)
				.ToListAsync();
		}
	}
}
