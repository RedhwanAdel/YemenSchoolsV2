using YemenSchoolsV1.Application.Features.Terms.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile
    {
        public void GetTermListMapping()
        {
            CreateMap<Term, GetTermsListResponse>().ForMember(dest => dest.AcademicYearName, opt => opt.MapFrom(src => src.AcademicYear.Name))
.ReverseMap();
        }
    }
}
