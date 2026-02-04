using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users
                            .FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (userToReturn == null) throw new AuthenticationException("User not found");

            return userToReturn;
        }
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User Id not found in claims.");

            return Guid.Parse(userId);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found");
            return email;
        }
        public static Guid GetEntityId(this ClaimsPrincipal user)
        {
            var entityId = user.FindFirstValue("EntityId") ?? throw new AuthenticationException("EntityId claim not found");
            return Guid.Parse(entityId);
        }
        public static string GetUserType(this ClaimsPrincipal user)
        {
            var userType = user.FindFirstValue("UserType") ?? throw new AuthenticationException("UserType claim not found");
            return userType;
        }


        public static Dictionary<string, string[]> ToErrorDictionary(this IdentityResult result)
        {
            return result.Errors
                .GroupBy(e => e.Code.Split('.')[0])
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());
        }
    }
}
