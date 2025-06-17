using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.AcademicYears
{
    public partial class YearProfile
    {
        public void GetYearsListMapping()
        {
            CreateMap<AcademicYear, GetYearListResponse>().ReverseMap();
        }
    }
}
