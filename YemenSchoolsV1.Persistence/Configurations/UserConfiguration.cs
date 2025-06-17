using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasMany(ur => ur.UserRoles)
					.WithOne(u => u.User)
					.HasForeignKey(u => u.UserId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
