using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile :Profile
    {
        public SectionsProfile()
        {
            CreateSectionMapping();
            EditSectionMapping();
            GetSectionsListMapping();
        }
    }
}
