using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Domain.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public ICollection<AppUserRole> UserRoles { get; set; } = [];

	}
}
