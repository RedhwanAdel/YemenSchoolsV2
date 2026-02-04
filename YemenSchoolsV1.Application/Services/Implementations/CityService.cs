using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;


namespace YemenSchoolsV1.Application.Services.Implementations
{
    public class CityService : ICityService
    {

        #region filed
        private readonly ICityRepository _cityRepository;

        #endregion

        #region constractor
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #endregion

        #region handel acrtions


        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }
        public async Task<City?> GetCityDetailsAsync(Guid id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }
        public async Task<City?> CreateCityAsync(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }
            return await _cityRepository.AddAsync(city);
        }
        public async Task<City?> EditCityAsync(Guid id, City city)
        {
            if (city == null)
            { 
                throw new ArgumentNullException(nameof(city));
            }
            var existingCity = await _cityRepository.GetByIdAsync(id);
            if (existingCity == null) { return null; }
            return await _cityRepository.UpdateAsync(id, city);
        }
        public async Task<bool> DeleteCityAsync(Guid id)
        {
            var city =await _cityRepository.GetByIdAsync(id);
            if (city == null)
                return false;
            return await _cityRepository.DeleteAsync(id);
        }
        #endregion


    }
}
