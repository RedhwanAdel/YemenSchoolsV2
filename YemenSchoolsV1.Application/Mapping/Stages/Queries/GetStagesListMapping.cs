using YemenSchoolsV1.Application.Features.Stages.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Stages
{
    public partial class StagesProfile
    {
        public void GetStagesListMapping()
        {
            CreateMap<Stage, GetStagesListResponse>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.NameEn))
                .ReverseMap();
        }
    }
}
