using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest<Response<bool>>
    {
        public DeleteSchoolCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
