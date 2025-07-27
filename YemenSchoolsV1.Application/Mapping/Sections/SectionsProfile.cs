using AutoMapper;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile : Profile
    {
        public SectionsProfile()
        {
            CreateMap<Section, SectionByGradeAndYearDto>()
             .ReverseMap();
            CreateMap<Section, CreateSectionDto>()
           .ReverseMap();
            CreateMap<SectionSubject, SectionSubjectInfoDto>()
           .ReverseMap();
            CreateMap<SectionSubject, CreateSectionSubjectDto>()
           .ReverseMap();
            CreateMap<Section, SectionDto>()
           .ReverseMap();
        }
    }
}
