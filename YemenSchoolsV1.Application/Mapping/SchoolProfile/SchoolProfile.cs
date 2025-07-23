using AutoMapper;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateSchoolMapping();
            GetSchoolPagenatedListMapping();
            GetSchoolDetailsMapping();
            CreateSchoolPhonsMapping();
            EditSchoolForAdminMapping();
            CreateMap<SchoolGrade, CreateSchoolGradeDto>()
             .ReverseMap();
        }
    }
}
