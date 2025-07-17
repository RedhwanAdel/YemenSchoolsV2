using AutoMapper;
using YemenSchoolsV1.Application.Features.Grades.Queries.GetGradeById;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Grades
{
    public partial class GradesProfile : Profile
    {
        public GradesProfile()
        {
            CreateGradeMapping();
            EditGradeMapping();
            GetYearsListMapping();
            CreateMap<Grade, GetGradeByIdResponse>().ReverseMap();

        }
    }
}
