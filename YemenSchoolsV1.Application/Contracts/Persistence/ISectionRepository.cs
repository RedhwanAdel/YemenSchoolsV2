using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISectionRepository : IGenericRepositoryAsync<Section>
    {
        Task<IEnumerable<Section>> GetSectionsByTeacherIdAsync(Guid teacherId);

        Task<IEnumerable<Section>> GetSectionsByAcademicYearAndSchoolGradeAsync(Guid academicYearId, Guid schoolGradeId);
        Task<Section?> GetSectionByIdAsync(Guid sectionId);
        Task<List<SectionSummaryDto>> GetSectionSummariesByAcademicYearAsync(Guid academicYearId);

    }
}
