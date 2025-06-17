using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Delete
{
    public class DeleteSectionCommand : IRequest<Response<bool>>
    {
        public DeleteSectionCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
