using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons;
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;

namespace YemenSchoolsV1.API.Controllers
{
    
    public class SchoolController : AppControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Paginated([FromQuery] GetSchoolPagenatedListQueary query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSchoolDetailsQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }

        [HttpPost("AddPhonsToSchool")]

        public async Task<IActionResult> AddPhonsToSchool([FromBody] CreateSchoolPhonsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteSchoolCommand(id));
            return NewResult(response);
        }
      
    }
}
