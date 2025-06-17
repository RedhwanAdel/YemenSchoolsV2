using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.CityProfile
{
    public partial class CityProfile
    {
        public void GetCitiesListMapping()
        {

            CreateMap<City, GetCitiesListResponse>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Localize(src.NameAr,src.NameEn) ))
                .ReverseMap();

        }



    }
}
