using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.CityProfile
{
    public partial class CityProfile
    {
        public void EditCityMapping()
        {
            CreateMap<City, EditCityCommand>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ReverseMap();
            CreateMap<City, EditCityResponse>()
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
             .ReverseMap();
        }
    }
}
