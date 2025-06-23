using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Accounts.Register;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
	public class AccountController(SignInManager<AppUser> signInManager) : AppControllerBase
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);
		}
		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return NoContent();
		}
		[HttpGet("user-info")]
		public async Task<ActionResult> GetUserInfo()
		{
			if (User.Identity?.IsAuthenticated == false) return NoContent();
			var user = await signInManager.UserManager.GetUserByEmail(User);
			return Ok(new
			{
				user.FirstName,
				user.LastName,
				user.Email,
			});

		}

		[HttpGet]
		public ActionResult GetAuthState()
		{
			return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
		}


	}
}
