using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommand : IRequest<Response<bool>>
    {
        public DeleteTermCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
