using AutoMapper;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Features.Sections;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile : Profile
    {
        public SectionsProfile()
        {
            CreateMap<Section, SectionByGradeAndYearDto>()
                .ForMember(dest => dest.ClassTeacherName,
                    opt => opt.MapFrom(src => src.ClassTeacher != null ? src.ClassTeacher.NameAr : null))
                .ForMember(dest => dest.GradeName,
                    opt => opt.MapFrom(src => src.SchoolGrade != null ? src.SchoolGrade.StageGrade.Grade.Name : null))
             .ReverseMap();
            CreateMap<Section, CreateSectionDto>()
           .ReverseMap();
            CreateMap<SectionSubject, SectionSubjectInfoDto>()
           .ReverseMap();
            CreateMap<SectionSubject, SectionSubjecUpdateDto>()
           .ReverseMap();
            CreateMap<SectionSubject, CreateSectionSubjectDto>()
           .ReverseMap();
            CreateMap<Section, SectionDto>()
           .ReverseMap();
        }
    }
}
