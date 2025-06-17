using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommand : IRequest<Response<bool>>
    {
        public DeleteRegionCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
