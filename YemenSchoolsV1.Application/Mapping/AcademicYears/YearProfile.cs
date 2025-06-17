using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
