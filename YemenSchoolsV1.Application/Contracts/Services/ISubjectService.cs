using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
	public interface ISubjectService
	{
		public Task<List<Subject>> GetAllSubjectsAsync();

		Task<List<Subject>> GetSubjectsBySchoolIdAsync(Guid schoolId);

		public Task<Subject?> GetSubjectDetailsAsync(Guid id);

		public Task<Subject?> CreateSubjectAsync(Subject subject);

		public Task<Subject?> EditSubjectAsync(Guid id, Subject subject);

		public Task<bool> DeleteSubjectAsync(Guid id);
	}
}