using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Sections.Commands.Create;
using YemenSchoolsV1.Application.Features.Sections.Commands.Delete;
using YemenSchoolsV1.Application.Features.Sections.Commands.Update;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetAll;
using YemenSchoolsV1.Application.Helpers;

namespace YemenSchoolsV1.API.Controllers
{

	public class SectionsController : AppControllerBase
	{
		[HttpGet("GetAllSectiosPaged/{schoolId:guid}")]
		public async Task<IActionResult> GetAll([FromRoute] Guid schoolId, [FromQuery] PaginationQuery paginationQuery)
		{
			var response = await Mediator.Send(new GetSectionsListQueary(paginationQuery, schoolId));
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateSectionCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditSectionCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new DeleteSectionCommand(id));
			return NewResult(response);
		}
	}
}
