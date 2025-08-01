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
            builder.HasIndex(p => p.NationalId)
               .IsUnique();

            builder.Property(p => p.NationalId).IsRequired().HasMaxLength(50); // مثال: تحديد طول
            builder.Property(p => p.NameAr).IsRequired().HasMaxLength(100);
            builder.Property(p => p.NameEn).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Email).HasMaxLength(100); // اختياري
            builder.Property(p => p.JobTitle).HasMaxLength(100); // اختياري
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.Gender).IsRequired(); // Enum


            builder.HasOne(p => p.User)
                   .WithOne()
                   .HasForeignKey<Parent>(p => p.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
