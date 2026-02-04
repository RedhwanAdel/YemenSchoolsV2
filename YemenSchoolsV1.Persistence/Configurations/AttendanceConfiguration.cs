using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            // العلاقة مع جدول AcademicYear
            builder.HasOne(a => a.AcademicYear)
                   .WithMany(ay => ay.Attendances)
                   .HasForeignKey(a => a.AcademicYearId)
                   .IsRequired();

            // العلاقة مع جدول Section
            builder.HasOne(a => a.Section)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(a => a.SectionId)
                   .IsRequired();

            // العلاقة مع جدول Teacher
            builder.HasOne(a => a.ClassTeacher)
                   .WithMany(t => t.Attendances)
                   .HasForeignKey(a => a.ClassTeacherId)
                   .IsRequired();

        }
    }
}
