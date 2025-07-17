using AutoMapper;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYearById;
using YemenSchoolsV1.Domain.Entities;
namespace YemenSchoolsV1.Application.Mapping.AcademicYears
{
    public partial class YearProfile : Profile
    {
        public YearProfile()
        {
            CreateYearMapping();
            EditYearMapping();
            GetYearsListMapping();
            CreateMap<AcademicYear, GetYearByIdResponse>().ReverseMap();

        }
    }
}
