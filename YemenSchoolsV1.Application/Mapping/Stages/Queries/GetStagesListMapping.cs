using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;
using YemenSchoolsV1.Application.Features.Stages.Queries.GetAll;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Mapping.Stages
{
    public partial class StagesProfile
    {
        public void GetStagesListMapping()
        {
            CreateMap<Stage, GetStagesListResponse>().ReverseMap();
        }
    }
}
