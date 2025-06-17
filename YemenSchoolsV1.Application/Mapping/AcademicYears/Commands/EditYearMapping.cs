using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.AcademicYears
{
    public partial class YearProfile
    {
        public void EditYearMapping()
        {
            CreateMap<AcademicYear, EditYearCommand>().ReverseMap();

        }
    }
}
