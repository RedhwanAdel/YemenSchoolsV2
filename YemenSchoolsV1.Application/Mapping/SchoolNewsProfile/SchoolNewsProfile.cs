using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.SchoolNewsProfile
{
    public partial class SchoolNewsProfile:Profile
    {
        public SchoolNewsProfile()
        {
            CreateSchoolNewsMapping();
            GetSchoolNewsListMapping();
            GetSchoolNewsDetails();
        }
    }
}
