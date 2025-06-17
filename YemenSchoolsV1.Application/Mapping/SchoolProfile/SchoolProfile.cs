using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.SchoolProfile
{
    public partial class SchoolProfile:Profile
    {
        public SchoolProfile()
        {
            CreateSchoolMapping();
            GetSchoolPagenatedListMapping();
            GetSchoolDetailsMapping();
            CreateSchoolPhonsMapping();
        }
    }
}
