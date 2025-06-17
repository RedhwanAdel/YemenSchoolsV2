using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Domain.Entities
{
	public class AppUserRole : IdentityUserRole<Guid>
	{
		public AppUser User { get; set; } = null!;
		public AppRole Role { get; set; } = null!;
	}
}
