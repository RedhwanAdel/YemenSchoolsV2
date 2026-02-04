using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class GradeSubjectConfiguration : IEntityTypeConfiguration<GradeSubject>
    {
        public void Configure(EntityTypeBuilder<GradeSubject> builder)
        {
            builder.HasKey(gs => gs.Id);

            builder.HasMany(gs => gs.SectionSubjects)
                .WithOne(ss => ss.GradeSubject)
                .HasForeignKey(ss => ss.GradeSubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
