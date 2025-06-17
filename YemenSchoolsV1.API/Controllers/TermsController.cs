using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm;
using YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm;
using YemenSchoolsV1.Application.Features.Terms.Commands.UpdateTerm;
using YemenSchoolsV1.Application.Features.Terms.Queries.GetAll;
using YemenSchoolsV1.Application.Helpers;

namespace YemenSchoolsV1.API.Controllers
{

	public class TermsController : AppControllerBase
	{
		[HttpGet("GetAllTermsPaged/{schoolId:guid}")]
		public async Task<IActionResult> GetAll([FromRoute] Guid schoolId, [FromQuery] PaginationQuery paginationQuery)
		{
			var response = await Mediator.Send(new GetTermsListQueary(paginationQuery, schoolId));
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateTermCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditTermCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new DeleteTermCommand(id));
			return NewResult(response);
		}
	}
}
