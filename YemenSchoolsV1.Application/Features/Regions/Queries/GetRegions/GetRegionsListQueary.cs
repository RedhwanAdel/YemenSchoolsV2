using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsListQueary : IRequest<Response<List<GetRegionsListResponse>>>
    {
    }
}
