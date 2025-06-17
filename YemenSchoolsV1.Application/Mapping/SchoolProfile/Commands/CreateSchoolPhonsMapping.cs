using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile
    {
        public void CreateSchoolPhonsMapping()
        {

            CreateMap<SchoolPhone, CreateSchoolPhonsCommand>()
                .ReverseMap();

        }
    }
}
