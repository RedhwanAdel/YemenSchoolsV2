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
		public static string GetEmail(this ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found");
			return email;
		}

		public static Dictionary<string, string[]> ToErrorDictionary(this IdentityResult result)
		{
			return result.Errors
				.GroupBy(e => e.Code.Split('.')[0])
				.ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());
		}
	}
}
