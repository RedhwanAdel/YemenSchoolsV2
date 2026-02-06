using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Accounts.Register;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

using YemenSchoolsV1.Application.Features.Accounts.Commands.ChangePassword;
using YemenSchoolsV1.Application.Features.Accounts.Commands.SessionLogin;
using YemenSchoolsV1.Application.Features.Accounts.Queries.GetUserInfo;

namespace YemenSchoolsV1.API.Controllers
{
    public class AccountController : AppControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var response = await Mediator.Send(new SessionLoginCommand(request.UserName, request.Password));
            return NewResult(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromServices] SignInManager<AppUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var response = await Mediator.Send(new GetUserInfoQuery(User));
            return NewResult(response);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            var userId = User.GetUserId();
            var response = await Mediator.Send(new ChangePasswordCommand(userId, model.CurrentPassword, model.NewPassword));
            return NewResult(response);
        }

        [HttpGet("auth-state")]
        public ActionResult GetAuthState()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }

    }
}
