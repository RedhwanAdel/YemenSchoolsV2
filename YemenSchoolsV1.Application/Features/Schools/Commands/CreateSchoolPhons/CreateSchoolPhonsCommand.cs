using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons
{
    public class CreateSchoolPhonsCommand : IRequest<Response<string>>
    {
        public Guid SchoolId { get; set; }
        public List<string> PhoneNumber { get; set; }
    }
}
