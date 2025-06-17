using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Domain.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public ICollection<AppUserRole> UserRoles { get; set; } = [];

	}
}
