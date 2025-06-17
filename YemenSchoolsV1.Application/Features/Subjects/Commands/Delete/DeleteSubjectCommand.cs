using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Delete
{
    public class DeleteSubjectCommand : IRequest<Response<bool>>
    {
        public DeleteSubjectCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
