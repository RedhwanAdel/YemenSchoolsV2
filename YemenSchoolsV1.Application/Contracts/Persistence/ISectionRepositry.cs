using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISectionRepositry : IGenericRepositoryAsync<Section>
    {
        Task<IEnumerable<Section>> GetSectionsByAcademicYearAndSchoolGradeAsync(Guid academicYearId, Guid schoolGradeId);
        Task<Section?> GetSectionByIdAsync(Guid sectionId);
        Task<List<SectionSummaryDto>> GetSectionSummariesByAcademicYearAsync(Guid academicYearId);

    }
}
