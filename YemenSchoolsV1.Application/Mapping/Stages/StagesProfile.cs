using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.Stages
{
    public partial class StagesProfile:Profile
    {
        public StagesProfile()
        {
            CreateStageMapping();
            EditStageMapping();
            GetStagesListMapping();

        }
    }
}
