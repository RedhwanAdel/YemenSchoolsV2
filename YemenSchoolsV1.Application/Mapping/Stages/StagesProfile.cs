using AutoMapper;
using YemenSchoolsV1.Application.Features.Stages.Queries.GetById;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Stages
{
    public partial class StagesProfile : Profile
    {
        public StagesProfile()
        {
            CreateStageMapping();
            EditStageMapping();
            GetStagesListMapping();
            CreateMap<Stage, GetStageByIdResponse>()
                             .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.NameEn))

                .ReverseMap();


        }
    }
}
