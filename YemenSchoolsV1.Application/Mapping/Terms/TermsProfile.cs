using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.Terms
{
    public partial class TermsProfile : Profile
    {
        public TermsProfile()
        {
            CreateTermMapping();
            EditTermMapping();
            GetTermListMapping();
        }
    }
}
