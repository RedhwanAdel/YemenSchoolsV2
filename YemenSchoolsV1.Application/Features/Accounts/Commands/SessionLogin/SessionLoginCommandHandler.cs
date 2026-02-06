using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Accounts.Commands.SessionLogin
{
    public class SessionLoginCommandHandler : ResponseHandler, IRequestHandler<SessionLoginCommand, Response<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SessionLoginCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<string>> Handle(SessionLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest<string>("Invalid username or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest<string>("Invalid username or password");
            }

            // حذف أي Claims قديمة مرتبطة بالمستخدم
            var existingClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in existingClaims.Where(c => c.Type == "EntityId" || c.Type == "UserType"))
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // إضافة Claims مخصصة (EntityId, UserType)
            await _userManager.AddClaimAsync(user, new Claim("EntityId", user.EntityId.ToString()));
            await _userManager.AddClaimAsync(user, new Claim("UserType", user.UserType));

            // تسجيل الدخول
            await _signInManager.SignInAsync(user, isPersistent: true);

            return Success("Login successful");
        }
    }
}
