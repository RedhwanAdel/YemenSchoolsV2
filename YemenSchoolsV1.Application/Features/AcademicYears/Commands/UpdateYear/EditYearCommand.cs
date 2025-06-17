using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear
{
    public class EditYearCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; } // تاريخ بداية العام الدراسي
        public DateTime? EndDate { get; set; } // تاريخ نهاية العام الدراسي
    }
}
