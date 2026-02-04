using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.RegisterNo)
              .IsUnique();



            builder.Property(s => s.RegisterNo).IsRequired().HasMaxLength(50);
            builder.Property(s => s.NameAr).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameEn).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Nationality).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Address).IsRequired().HasMaxLength(250);
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.Gender).IsRequired();
            builder.Property(s => s.ProfileImage).HasMaxLength(250); // اختياري
            builder.Property(s => s.PhoneNumber).HasMaxLength(20); // اختياري
            builder.Property(s => s.Email).HasMaxLength(100); // اختياري
            builder.Property(s => s.CreatedTime).IsRequired();
            builder.Property(s => s.IsActive).IsRequired();


            builder.HasOne(s => s.User)
                   .WithOne()
                   .HasForeignKey<Student>(s => s.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
            // تعريف العلاقات One-to-Many
            builder.HasOne(s => s.School)
                  .WithMany(sc => sc.Students) // هنا نحدد الخاصية العكسية في School
                  .HasForeignKey(s => s.SchoolId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.CurrentAcademicYear)
                   .WithMany(ay => ay.Students) // هنا نحدد الخاصية العكسية في AcademicYear
                   .HasForeignKey(s => s.CurrentAcademicYearId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.CurrentSection)
                   .WithMany(sec => sec.Students) // هنا نحدد الخاصية العكسية في Section
                   .HasForeignKey(s => s.CurrentSectionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);




        }
    }

}
