using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Update
{
    public class EditSectionCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GradeId { get; set; }

        public int? RoomNumber { get; set; }
    }
}
