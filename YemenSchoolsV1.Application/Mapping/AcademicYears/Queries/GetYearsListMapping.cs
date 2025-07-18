using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.AcademicYears
{
    public partial class YearProfile
    {
        public void GetYearsListMapping()
        {
            CreateMap<AcademicYear, GetYearListResponse>()
                                .ForMember(dest => dest.StageName, opt => opt.MapFrom(src => src.Stage.Name))
.ReverseMap();
        }
    }
}
