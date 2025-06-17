using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Domain.Entities
{
	public class AppRole : IdentityRole<Guid>
	{
		public ICollection<AppUserRole> UserRoles { get; set; } = [];
	}
}
