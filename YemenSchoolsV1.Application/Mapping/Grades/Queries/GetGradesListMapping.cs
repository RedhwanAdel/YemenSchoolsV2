using YemenSchoolsV1.Application.Features.Grades.Queries;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Grades
{
    public partial class GradesProfile
    {
        public void GetYearsListMapping()
        {
            CreateMap<Grade, GetGradesListResponse>().ForMember(dest => dest.TermName, opt => opt.MapFrom(src => src.Term.Name))
.ReverseMap();
        }
    }
}
