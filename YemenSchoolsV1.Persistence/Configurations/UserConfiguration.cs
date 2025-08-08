using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(100); // اختياري
            builder.Property(u => u.LastName).HasMaxLength(100); // اختياري


            builder.Property(u => u.EntityId).IsRequired(); // يجب أن يرتبط بحساب مستخدم بكيان فعلي
            builder.Property(u => u.UserType).IsRequired().HasMaxLength(50);


            builder.HasMany(ur => ur.UserRoles)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
