using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Teachers.Commands.Create;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.TeacherProfile
{
    public partial class TeacherProfile
    {
        public void CreateTeacherMapping()
        {
            CreateMap<Teacher, CreateTeacherCommand>()
                .ReverseMap();

          
        }
    }
}

