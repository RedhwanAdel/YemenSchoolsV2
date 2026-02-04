using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class TermConfiguration : IEntityTypeConfiguration<Term>
    {
        public void Configure(EntityTypeBuilder<Term> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(s => s.SectionSubjects)
                .WithOne(ss => ss.Term)
                .HasForeignKey(ss => ss.TermId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
