using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommand : IRequest<Response<CreateRegionResponse>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Image { get; set; }
        public Guid CityId { get; set; }

    }
}
