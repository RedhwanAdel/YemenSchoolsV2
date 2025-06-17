using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.SubjectProfile
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            GetSubjectsListMapping();
            CreateSubjectMapping();
            GetSubjectDetailsMapping();
            EditSubjectMapping();
        }
    }
}

