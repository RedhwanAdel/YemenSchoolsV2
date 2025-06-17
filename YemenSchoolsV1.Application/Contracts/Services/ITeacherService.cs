using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
	public interface ITeacherService
	{
		Task<List<Teacher>> GetAllTeachersAsync();

		Task<List<Teacher>> GetTeachersBySchoolIdAsync(Guid schoolId);

		Task<Teacher?> GetTeacherDetailsAsync(Guid id);

		Task<Teacher?> CreateTeacherAsync(Teacher teacher);

		Task<Teacher?> EditTeacherAsync(Guid id, Teacher teacher);

		Task<bool> DeleteTeacherAsync(Guid id);
	}
}