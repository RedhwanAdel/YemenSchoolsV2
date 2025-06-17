using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Subjects.Commands.Create;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.SubjectProfile
{
    public partial class SubjectProfile
    {
        public void CreateSubjectMapping()
        {
            CreateMap<Subject, CreateSubjectCommand>()
                .ReverseMap();

            
        }
    }
}

