using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IRegionRepositry : IGenericRepositoryAsync<Region>
    {
        Task<List<Region>?> GetRegionByCityIdIncludeAsync(Guid cityId);
        Task<List<Region>> getAllRegionIncludeAsync();

        Task<int?> GetSchoolCount(Guid regionId);

    }
}
