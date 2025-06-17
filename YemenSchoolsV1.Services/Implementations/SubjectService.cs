using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Services.Implementations
{
	internal class SubjectService : ISubjectService
	{
		private readonly ISubjectRepositry subjectRepository;

		public SubjectService(ISubjectRepositry subjectRepository)
		{
			this.subjectRepository = subjectRepository;
		}

		public async Task<Subject?> CreateSubjectAsync(Subject subject)
		{
			if (subject == null)
			{
				throw new ArgumentNullException(nameof(subject));
			}
			return await subjectRepository.AddAsync(subject);
		}

		public async Task<bool> DeleteSubjectAsync(Guid id)
		{
			var subject = await subjectRepository.GetByIdAsync(id);
			if (subject == null)
				return false;
			return await subjectRepository.DeleteAsync(id);
		}

		public async Task<Subject?> EditSubjectAsync(Guid id, Subject subject)
		{
			if (subject == null)
			{
				throw new ArgumentNullException(nameof(subject));
			}
			var existingSubject = await subjectRepository.GetByIdAsync(id);
			if (existingSubject == null) { return null; }
			return await subjectRepository.UpdateAsync(id, subject);
		}

		public async Task<List<Subject>> GetAllSubjectsAsync()
		{
			return await subjectRepository.GetAllAsync();
		}

		public async Task<List<Subject>> GetSubjectsBySchoolIdAsync(Guid schoolId)
		{
			return await subjectRepository.GetAllBySchoolIdAsync(schoolId);
		}

		public async Task<Subject?> GetSubjectDetailsAsync(Guid id)
		{
			return await subjectRepository.GetByIdAsync(id);
		}
	}
}