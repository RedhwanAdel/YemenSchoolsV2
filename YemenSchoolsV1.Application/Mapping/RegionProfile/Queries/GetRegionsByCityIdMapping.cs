using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.RegionProfile
{
    public partial class RegionProfile
    {
        public void GetRegionsByCityIdMapping()
        {
            CreateMap<Region, GetRegionsByCityIdResponse>()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameAr))
               .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.NameAr))
               .ReverseMap();

        }
    }
}
