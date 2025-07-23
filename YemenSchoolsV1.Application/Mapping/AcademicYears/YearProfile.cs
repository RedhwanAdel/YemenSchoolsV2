using AutoMapper;
namespace YemenSchoolsV1.Application.Mapping.AcademicYears
{
    public partial class YearProfile : Profile
    {
        public YearProfile()
        {
            CreateYearMapping();
            EditYearMapping();
            GetYearsListMapping();

        }
    }
}
