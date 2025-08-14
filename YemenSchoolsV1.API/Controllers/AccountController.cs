using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Accounts.Register;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager, ITeacherRepositry teacherRepositry, UserManager<AppUser> _userManager) : AppControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password");

            // حذف أي Claims قديمة مرتبطة بالمستخدم
            var existingClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in existingClaims.Where(c => c.Type == "EntityId" || c.Type == "UserType"))
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // إضافة Claims مخصصة (EntityId, UserType)
            await _userManager.AddClaimAsync(user, new Claim("EntityId", user.EntityId.ToString()));
            await _userManager.AddClaimAsync(user, new Claim("UserType", user.UserType));

            // تسجيل الدخول (سيقوم ASP.NET Core Identity ببناء الكوكي مع كل الـ Claims)
            await signInManager.SignInAsync(user, isPersistent: true);

            return Ok(new { message = "Login successful" });
        }



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
            if (user == null) return Unauthorized();
            if (user.UserType == "Teacher")
            {
                var teacher = await teacherRepositry.GetByIdAsync(user.EntityId);
                if (teacher == null) return NotFound();
                return Ok(new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.EntityId,
                    user.UserType,
                    teacher.SchoolId
                });


            }
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.EntityId,
                user.UserType,
                user.SchoolId
            });

        }

        [HttpGet]
        public ActionResult GetAuthState()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }


    }
}
