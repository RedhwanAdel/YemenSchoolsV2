using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear
{
    public class DeleteYearCommand : IRequest<Response<bool>>
    {
        public DeleteYearCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
