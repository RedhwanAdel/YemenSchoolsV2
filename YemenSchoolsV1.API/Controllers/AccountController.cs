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

namespace YemenSchoolsV1.API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager, ITeacherRepository teacherRepository, UserManager<AppUser> _userManager, YemenShoolsDbContext context) : AppControllerBase
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
                var teacher = await teacherRepository.GetByIdAsync(user.EntityId);
                if (teacher == null) return NotFound();
                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.ImageUrl,
                    user.Email,
                    user.EntityId,
                    user.UserType,
                    teacher.SchoolId
                });


            }
            return Ok(new
            {
                user.Id,
                user.Name,
                user.ImageUrl,
                user.Email,
                user.EntityId,
                user.UserType,
                user.SchoolId
            });

        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound("المستخدم غير موجود");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            // تحديث جلسة الدخول الحالية
            await signInManager.RefreshSignInAsync(user);

            return Ok(new { message = "تم تغيير كلمة المرور بنجاح" });
        }


        [HttpGet]
        public ActionResult GetAuthState()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }


        [HttpPut("update-profile")]
        //[Authorize(Roles = "Parent")] // أو حسب نوع المستخدم
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateParentProfileDto model)
        {
            var userType = User.GetUserType();
            if (userType != "Parent")
                return Unauthorized();
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized();


            // اجلب الـ AppUser
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            // اجلب الـ Parent المرتبط
            var parent = await context.Parents.FirstOrDefaultAsync(p => p.UserId == userId);
            if (parent == null)
                return NotFound("Parent not found.");

            // تحديث AppUser
            if (!string.IsNullOrWhiteSpace(model.Name))
                user.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
                user.ImageUrl = model.ImageUrl;

            // تحديث Parent
            parent.PhoneNumber = model.PhoneNumber ?? parent.PhoneNumber;
            parent.Address = model.Address ?? parent.Address;
            parent.Email = model.Email ?? parent.Email;
            parent.JobTitle = model.JobTitle ?? parent.JobTitle;

            // تحديث القيم
            await _userManager.UpdateAsync(user);
            context.Parents.Update(parent);
            await context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully." });
        }
        [HttpGet("profile")]
        //[Authorize(Roles = "Parent")]
        public async Task<IActionResult> GetProfile()
        {
            var userType = User.GetUserType();
            if (userType != "Parent")
                return Unauthorized();

            var userId = User.GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized();


            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return NotFound("User not found.");

            var parent = await context.Parents.FirstOrDefaultAsync(p => p.UserId == userId);
            if (parent == null)
                return NotFound("Parent not found.");

            var dto = new ParentProfileDto
            {
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                PhoneNumber = parent.PhoneNumber,
                Address = parent.Address,
                Email = parent.Email,
                JobTitle = parent.JobTitle
            };

            return Ok(dto);
        }


    }
}
