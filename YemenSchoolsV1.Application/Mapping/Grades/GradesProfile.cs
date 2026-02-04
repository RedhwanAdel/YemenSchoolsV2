using AutoMapper;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Features.Grades.Queries;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Grades
{
    public partial class GradesProfile : Profile
    {
        public GradesProfile()
        {
            CreateGradeMapping();
            EditGradeMapping();
            CreateMap<Grade, GetGradesListResponse>().ReverseMap();
            CreateMap<StageGrade, StageGradeDto>()
                      .ForMember(dest => dest.StageName, opt => opt.MapFrom(src => src.Stage.Name))
                      .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
                      .ReverseMap();

        }
    }
}
