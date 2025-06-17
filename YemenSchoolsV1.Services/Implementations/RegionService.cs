using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Services.Implementations
{
   public class RegionService:IRegionService
    {

        #region filed
        private readonly IRegionRepositry regionRepositry;

        #endregion

        #region constractor
        public RegionService(IRegionRepositry regionRepositry)
        {
            this.regionRepositry = regionRepositry;
        }
        #endregion

        #region handel acrtions


        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await regionRepositry.GetAllAsync();
        }
        public async Task<Region?> GetRegionDetailsAsync(Guid id)
        {
            return await regionRepositry.GetByIdAsync(id);
        }
        public async Task<Region?> CreateRegionAsync(Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            return await regionRepositry.AddAsync(region);
        }
        public async Task<Region?> EditRegionAsync(Guid id, Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            var existingregion = await regionRepositry.GetByIdAsync(id);
            if (existingregion == null) { return null; }
            return await regionRepositry.UpdateAsync(id, region);
        }
        public async Task<bool> DeleteRegionAsync(Guid id)
        {
            var region = await regionRepositry.GetByIdAsync(id);
            if (region == null)
                return false;
            return await regionRepositry.DeleteAsync(id);
        }

        public async Task<List<Region>> GetAllRegionsByCityIdAsync(Guid cityId)
        {

            return await regionRepositry.GetRegionByCityIdIncludeAsync(cityId);

        }

        public async Task<int?> GetAllSchoolCountAsync(Guid regionId)
        {

            return await regionRepositry.GetSchoolCount(regionId);

        }

        #endregion
    }
}
