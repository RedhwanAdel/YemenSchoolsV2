using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasMany(ur => ur.UserRoles)
					.WithOne(u => u.Role)
					.HasForeignKey(u => u.RoleId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
