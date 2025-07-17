using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IAcademicYearRepository : IGenericRepositoryAsync<AcademicYear>
    {
        Task<AcademicYear?> GetAcadmicYearByIdIncludeAsync(Guid id);

    }
}
