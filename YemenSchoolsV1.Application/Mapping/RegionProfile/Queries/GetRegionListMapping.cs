using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.RegionProfile
{
    public partial class RegionProfile
    {

        public void GetRegionListMapping()
        {
            CreateMap<Region, GetRegionsListResponse>()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                              .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Localize(src.City.NameAr, src.City.NameEn)))

               .ReverseMap();

        }
    }
}
