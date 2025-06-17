using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Students.Commands.Create;
using YemenSchoolsV1.Application.Features.Students.Queries.GetById;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsPaged;
using YemenSchoolsV1.Application.Helpers;

namespace YemenSchoolsV1.API.Controllers
{
	public class StudentController : AppControllerBase
	{
		[HttpGet("GetAllStudentsPaged/{schoolId:guid}")]
		public async Task<IActionResult> GetPaged([FromRoute] Guid schoolId, [FromQuery] PaginationQuery paginationQuery)
		{
			var query = new GetStudentsPagedQuery(paginationQuery, schoolId);
			var result = await Mediator.Send(query);
			return Ok(result);
		}


		[HttpGet("GetStudentById/{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new GetStudentByIdQuery(id));
			return NewResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}


	}
}
