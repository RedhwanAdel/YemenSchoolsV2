using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Teachers.Commands.Update;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.TeacherProfile
{
    public partial class TeacherProfile
    {
        public void EditTeacherMapping()
        {
            CreateMap<Teacher, EditTeacherCommand>()
                .ReverseMap();

        }
    }
}

