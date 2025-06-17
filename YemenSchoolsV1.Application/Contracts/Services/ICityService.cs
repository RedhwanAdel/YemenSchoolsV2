using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ICityService
    {

        public Task<List<City>> GetAllCitiesAsync();
        public Task<City?> GetCityDetailsAsync(Guid id);
        public Task<City?> CreateCityAsync(City city);
        public Task<City?> EditCityAsync(Guid id, City city);
        public Task<bool> DeleteCityAsync(Guid id);

    }
}
