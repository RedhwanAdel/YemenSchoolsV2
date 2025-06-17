using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Sections
{
    public partial class SectionsProfile
    {
        public void GetSectionsListMapping()
        {
            CreateMap<Section, GetSectionsListResponse>().ReverseMap();
        }
    }
}
