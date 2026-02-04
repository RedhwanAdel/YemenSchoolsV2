using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IAcademicYearRepository : IGenericRepositoryAsync<AcademicYear>
    {
        Task<List<AcademicYear>> GetYearsBySchoolIdAsync(Guid id);
        Task<Guid?> SetCurrentYearAsync(Guid schoolId, Guid academicYearId);
        Task<Guid?> GetCurrentYearIdAsync(Guid schoolId);


    }
}
