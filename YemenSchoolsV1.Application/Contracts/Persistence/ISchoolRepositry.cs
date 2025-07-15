using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolRepositry : IGenericRepositoryAsync<School>
    {
        IQueryable<School> GetSchoolsWithCityAndRegionQueryable();
        Task<School?> GetSchoolDetailsInculdeAsync(Guid cityId);
        Task CreateSchoolPhonesRangAsync(List<SchoolPhone> schoolPhones);
        Task<SchoolForUpdate?> GetSchoolByIdForUpdateAsync(Guid schoolId);


    }
}
