using YemenSchoolsV1.Application.Features.Sections.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile
    {
        public void GetSectionsListMapping()
        {
            CreateMap<Section, GetSectionsListResponse>().ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
.ReverseMap();
        }
    }
}
