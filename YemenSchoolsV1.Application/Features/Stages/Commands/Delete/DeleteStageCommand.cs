using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Delete
{
    public class DeleteStageCommand : IRequest<Response<bool>>
    {
        public DeleteStageCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
