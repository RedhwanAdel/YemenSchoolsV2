using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.HasKey(ay => ay.Id);
            builder.Property(ay => ay.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(ay => ay.Sections)
                .WithOne(cs => cs.AcademicYear)
                .HasForeignKey(cs => cs.AcademicYearId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ay => ay.Terms)
              .WithOne(s => s.AcademicYear)
              .HasForeignKey(s => s.AcademicYearId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
