using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);

            builder.ToTable("Subjects");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.GradeSubjects)
                 .WithOne(gs => gs.Subject)
                 .HasForeignKey(gs => gs.SubjectId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "القرآن الكريم" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "التربية الإسلامية" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "اللغة العربية" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "الرياضيات" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Name = "العلوم" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Name = "الاجتماعيات" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Name = "اللغة الإنجليزية" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Name = "التاريخ" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Name = "الجغرافيا" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), Name = "الوطنية" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000011"), Name = "الجبر" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000012"), Name = "الهندسة" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000013"), Name = "الكيمياء" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000014"), Name = "الأحياء" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000015"), Name = "الفيزياء" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000016"), Name = "الرسم" },
           new Subject { Id = Guid.Parse("10000000-0000-0000-0000-000000000017"), Name = "الحاسوب" }
       );
        }
    }
}