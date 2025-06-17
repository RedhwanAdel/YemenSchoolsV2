using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public Guid CityId { get; set; }
        public int? countSchools { get; set; }

    }
}
