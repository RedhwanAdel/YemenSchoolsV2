using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Delete
{
    public class DeleteGradeCommand : IRequest<Response<bool>>
    {
        public DeleteGradeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
