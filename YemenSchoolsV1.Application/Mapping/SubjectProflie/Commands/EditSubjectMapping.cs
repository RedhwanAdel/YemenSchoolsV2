using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Subjects.Commands.Update;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SubjectProfile
{
    public partial class SubjectProfile
    {
        public void EditSubjectMapping()
        {
            CreateMap<Subject, EditSubjectCommand>()
                .ReverseMap();

            
        }
    }
}

