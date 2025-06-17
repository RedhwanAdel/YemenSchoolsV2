using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Services.Implementations
{
	public class TeacherService : ITeacherService
	{
		private readonly ITeacherRepositry teacherRepository;

		public TeacherService(ITeacherRepositry teacherRepository)
		{
			this.teacherRepository = teacherRepository;
		}

		public async Task<Teacher?> CreateTeacherAsync(Teacher teacher)
		{
			if (teacher == null)
			{
				throw new ArgumentNullException(nameof(teacher));
			}
			return await teacherRepository.AddAsync(teacher);
		}

		public async Task<bool> DeleteTeacherAsync(Guid id)
		{
			var teacher = await teacherRepository.GetByIdAsync(id);
			if (teacher == null)
				return false;
			return await teacherRepository.DeleteAsync(id);
		}

		public async Task<Teacher?> EditTeacherAsync(Guid id, Teacher teacher)
		{
			if (teacher == null)
			{
				throw new ArgumentNullException(nameof(teacher));
			}
			var existingTeacher = await teacherRepository.GetByIdAsync(id);
			if (existingTeacher == null) { return null; }
			return await teacherRepository.UpdateAsync(id, teacher);
		}

		public async Task<List<Teacher>> GetAllTeachersAsync()
		{
			return await teacherRepository.GetAllAsync();
		}

		public async Task<List<Teacher>> GetTeachersBySchoolIdAsync(Guid schoolId)
		{
			return await teacherRepository.GetAllBySchoolIdAsync(schoolId);
		}

		public async Task<Teacher?> GetTeacherDetailsAsync(Guid id)
		{
			return await teacherRepository.GetByIdAsync(id);
		}
	}
}