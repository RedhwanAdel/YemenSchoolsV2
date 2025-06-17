using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


    }
}
