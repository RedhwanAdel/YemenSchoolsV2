using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Mapping.RegionProfile
{
    public partial class RegionProfile : Profile
    {

        public RegionProfile()
        {
            CreateRegionMapping();
            GetRegionListMapping();
            GetRegionsByCityIdMapping();
            GetRegionDetailsMapping();

        }
    }
}
