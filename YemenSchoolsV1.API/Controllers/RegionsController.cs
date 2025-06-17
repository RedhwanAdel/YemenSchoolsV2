using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails;
using YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion;
using YemenSchoolsV1.Application.Features.Regions.Commands.DeleteRegion;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId;

namespace YemenSchoolsV1.API.Controllers
{

   
    public class RegionsController : AppControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetRegionsListQueary());
            return NewResult(response);
        }

        [HttpGet("GetAllRegionsByCityID/{cityId:guid}")]
        

        public async Task<IActionResult> GetAllRegionsByCityID([FromRoute] Guid cityId)
        {
            var response = await Mediator.Send(new GetRegionsByCityIdQueary(cityId));
            return NewResult(response);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetRegionDetailsQueary(id));
            return NewResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteRegionCommand(id));
            return NewResult(response);
        }
    }
}
