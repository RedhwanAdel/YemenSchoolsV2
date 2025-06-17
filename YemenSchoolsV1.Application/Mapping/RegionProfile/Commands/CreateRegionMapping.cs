using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.RegionProfile
{
    public partial class RegionProfile
    {
        public void CreateRegionMapping()
        {

            CreateMap<Region, CreateRegionCommand>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ReverseMap();
            CreateMap<Region, CreateRegionResponse>()
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
             .ReverseMap();



        }
    }
}
