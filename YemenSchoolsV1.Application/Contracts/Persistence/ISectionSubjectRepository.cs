using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISectionSubjectRepository : IGenericRepositoryAsync<SectionSubject>
    {
        Task<List<SectionSubjectInfoDto>> GetSectionSubjectsInfoBySectionIdAsync(Guid sectionId);

    }
}
