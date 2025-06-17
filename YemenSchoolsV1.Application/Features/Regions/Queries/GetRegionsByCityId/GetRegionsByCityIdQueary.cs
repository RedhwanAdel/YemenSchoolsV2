using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId
{
    public class GetRegionsByCityIdQueary : IRequest<Response<List<GetRegionsByCityIdResponse>>>
    {

        public GetRegionsByCityIdQueary(Guid cityId)
        {
            CityId = cityId;
        }
        public Guid CityId { get; set; }
    }
}
