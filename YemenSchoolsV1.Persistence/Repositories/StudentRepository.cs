using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
	public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
	{
		public StudentRepository(YemenShoolsDbContext dbContext) : base(dbContext)
		{

		}
	}
}
