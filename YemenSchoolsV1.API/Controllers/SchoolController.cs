using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons;
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.UpdateSchool;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;

namespace YemenSchoolsV1.API.Controllers
{

    public class SchoolController(ISchoolRepositry schoolRepositry) : AppControllerBase
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

        [HttpGet("GetSchoolByIdForUpdate/{id}")]
        public async Task<IActionResult> GetSchoolByIdForUpdate([FromRoute] Guid id)
        {
            var school = await schoolRepositry.GetSchoolByIdForUpdateAsync(id);
            if (school == null)
            {
                return NotFound(new { Message = "School not found" });
            }
            return Ok(school);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditSchoolForAdminCommand command)
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
