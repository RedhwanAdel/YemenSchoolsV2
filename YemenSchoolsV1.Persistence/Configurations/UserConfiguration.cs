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

            builder.HasOne(u => u.ParentEntity)
             .WithOne(p => p.User) // ربط AppUser بـ Parent
             .HasForeignKey<Parent>(p => p.UserId) // المفتاح الخارجي في Parent
             .IsRequired(false); // العلاقة اختيارية من جانب AppUser (لأن ليس كل AppUser هو Parent)

            builder.HasOne(u => u.StudentEntity)
                   .WithOne(s => s.User) // ربط AppUser بـ Student
                   .HasForeignKey<Student>(s => s.UserId) // المفتاح الخارجي في Student
                   .IsRequired(false); // العلاقة اختيارية من جانب AppUser (لأن ليس كل AppUser هو Student)


            builder.HasMany(ur => ur.UserRoles)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
