using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Features.Grades.Commands.Update;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Grades
{
    public partial class GradesProfile
    {
        public void EditGradeMapping()
        {
            CreateMap<Grade, EditGradeCommand>().ReverseMap();

        }
    }
}
