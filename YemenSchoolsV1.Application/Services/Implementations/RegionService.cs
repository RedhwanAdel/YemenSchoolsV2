using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Services.Implementations
{
    public class RegionService : IRegionService
    {

        #region filed
        private readonly IRegionRepository regionRepository;

        #endregion

        #region constractor
        public RegionService(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        #endregion

        #region handel acrtions


        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await regionRepository.getAllRegionIncludeAsync();
        }
        public async Task<Region?> GetRegionDetailsAsync(Guid id)
        {
            return await regionRepository.GetByIdAsync(id);
        }
        public async Task<Region?> CreateRegionAsync(Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            return await regionRepository.AddAsync(region);
        }
        public async Task<Region?> EditRegionAsync(Guid id, Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            var existingregion = await regionRepository.GetByIdAsync(id);
            if (existingregion == null) { return null; }
            return await regionRepository.UpdateAsync(id, region);
        }
        public async Task<bool> DeleteRegionAsync(Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region == null)
                return false;
            return await regionRepository.DeleteAsync(id);
        }

        public async Task<List<Region>> GetAllRegionsByCityIdAsync(Guid cityId)
        {

            return await regionRepository.GetRegionByCityIdIncludeAsync(cityId);

        }

        public async Task<int?> GetAllSchoolCountAsync(Guid regionId)
        {

            return await regionRepository.GetSchoolCount(regionId);

        }

        #endregion
    }
}
