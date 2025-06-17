using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IRegionService
    {
        public Task<List<Region>> GetAllRegionsAsync();
        public Task<List<Region>> GetAllRegionsByCityIdAsync(Guid cityId);
        public Task<Region?> GetRegionDetailsAsync(Guid id);
        public Task<Region?> CreateRegionAsync(Region region);
        public Task<Region?> EditRegionAsync(Guid id, Region region);
        public Task<bool> DeleteRegionAsync(Guid id);
        Task<int?> GetAllSchoolCountAsync(Guid regionId);

    }
}
