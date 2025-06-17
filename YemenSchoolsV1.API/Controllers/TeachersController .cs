using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Teachers.Commands.Create;
using YemenSchoolsV1.Application.Features.Teachers.Commands.Delete;
using YemenSchoolsV1.Application.Features.Teachers.Commands.Update;
using YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId;
using YemenSchoolsV1.Application.Features.Teachers.Queries.GetById;

namespace YemenSchoolsV1.API.Controllers
{
	public class TeachersController : AppControllerBase
	{
		//[HttpGet]
		//public async Task<IActionResult> GetAll()
		//{
		//    var response = await Mediator.Send(new GetTeachersListQuery());
		//    return NewResult(response);
		//}

		[HttpGet("GeAlltBySchoolId/{id:guid}")]
		public async Task<IActionResult> GetBySchoolId([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new GetTeachersListQuery(id));
			return NewResult(response);
		}

		[HttpGet("GetTeacherById/{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new GetTeacherDetailsQuery(id));
			return NewResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateTeacherCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditTeacherCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new DeleteTeacherCommand(id));
			return NewResult(response);
		}
	}
}