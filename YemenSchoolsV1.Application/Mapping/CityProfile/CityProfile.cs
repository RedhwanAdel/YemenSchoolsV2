using AutoMapper;

namespace YemenSchoolsV1.Application.Mapping.CityProfile
{



    public partial class CityProfile:Profile
    {

      
        public CityProfile()
        {

            GetCitiesListMapping();
            CreateCityMapping();
            GetCityDetailsMapping();
            EditCityMapping();
        }
    }
}
