using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class ParentConfiguration : IEntityTypeConfiguration<Parent>
	{
		public void Configure(EntityTypeBuilder<Parent> builder)
		{
			builder.HasKey(p => p.Id);

			builder.HasOne(p => p.User)
				   .WithOne()
				   .HasForeignKey<Parent>(p => p.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(p => p.Students)
				   .WithOne(ps => ps.Parent)
				   .HasForeignKey(ps => ps.ParentId);

			builder.Property(p => p.NameAr).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Email).HasMaxLength(100);
		}
	}
}
