using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Create
{
    public class CreateSectionCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public Guid GradeId { get; set; } 

        public int? RoomNumber { get; set; }
    }
}
