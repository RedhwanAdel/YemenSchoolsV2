using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class ParentStudentConfiguration : IEntityTypeConfiguration<ParentStudent>
    {
        public void Configure(EntityTypeBuilder<ParentStudent> builder)
        {
            builder.HasKey(ps => ps.Id);
            builder.HasIndex(ps => new { ps.ParentId, ps.StudentId })
               .IsUnique();
            builder.Property(ps => ps.RelationType).IsRequired().HasMaxLength(50);
            builder.Property(ps => ps.IsPrimaryContact).IsRequired();

            builder.HasOne(ps => ps.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(ps => ps.ParentId)
                .IsRequired();

            // تعريف العلاقة One-to-Many من Student إلى ParentStudent
            builder.HasOne(ps => ps.Student)
                   .WithMany(s => s.Parents)
                   .HasForeignKey(ps => ps.StudentId)
                   .IsRequired();
        }
    }

}
