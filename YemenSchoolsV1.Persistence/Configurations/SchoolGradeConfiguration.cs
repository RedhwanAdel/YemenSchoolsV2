using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class SchoolGradeConfiguration : IEntityTypeConfiguration<SchoolGrade>
    {
        public void Configure(EntityTypeBuilder<SchoolGrade> builder)
        {
            builder.HasKey(sg => sg.Id);

            builder.HasMany(sg => sg.GradeSubjects)
                .WithOne(gs => gs.SchoolGrade)
                .HasForeignKey(gs => gs.SchoolGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sg => sg.Sections)
                .WithOne(cs => cs.SchoolGrade)
                .HasForeignKey(cs => cs.SchoolGradeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(sg => sg.School)
                .WithMany(s => s.SchoolGrades)
                .HasForeignKey(sg => sg.SchoolId);
        }
    }
}
