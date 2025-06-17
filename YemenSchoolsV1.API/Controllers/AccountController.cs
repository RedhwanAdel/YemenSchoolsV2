using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Accounts.Login;
using YemenSchoolsV1.Application.Features.Accounts.Register;

namespace YemenSchoolsV1.API.Controllers
{
	public class AccountController : AppControllerBase
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
	}
}
