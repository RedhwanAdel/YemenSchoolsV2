using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId
{
    public class GetRegionsByCityIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string CityName { get; set; }
    }
}
