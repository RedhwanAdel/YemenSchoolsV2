using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.DeleteSchoolNews
{
    public class DeleteSchoolNewsCommand : IRequest<Response<bool>>
    {
        public DeleteSchoolNewsCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
