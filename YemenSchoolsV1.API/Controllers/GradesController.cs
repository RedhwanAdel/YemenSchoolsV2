using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Grades.Commands.Create;
using YemenSchoolsV1.Application.Features.Grades.Commands.Delete;
using YemenSchoolsV1.Application.Features.Grades.Commands.Update;
using YemenSchoolsV1.Application.Features.Grades.Queries;
using YemenSchoolsV1.Application.Helpers;

namespace YemenSchoolsV1.API.Controllers
{

	public class GradesController : AppControllerBase
	{
		[HttpGet("GetAllGradesPaged/{schoolId:guid}")]
		public async Task<IActionResult> GetAll([FromRoute] Guid schoolId, [FromQuery] PaginationQuery paginationQuery)
		{
			var response = await Mediator.Send(new GetGradesListQueary(paginationQuery, schoolId));
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateGradeCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditGradeCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new DeleteGradeCommand(id));
			return NewResult(response);
		}
	}
}
