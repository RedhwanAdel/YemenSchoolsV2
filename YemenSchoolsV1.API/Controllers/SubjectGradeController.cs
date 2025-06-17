using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.SubjectGrades.Commands.Create;

namespace YemenSchoolsV1.API.Controllers
{
	public class SubjectGradeController : AppControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateSubjectGradeCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
	}
}
