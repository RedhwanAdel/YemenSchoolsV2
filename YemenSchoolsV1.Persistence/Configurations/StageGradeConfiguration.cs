using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class StageGradeConfiguration : IEntityTypeConfiguration<StageGrade>
    {
        public void Configure(EntityTypeBuilder<StageGrade> builder)
        {


            builder
                .HasMany(sg => sg.SchoolGrades)
                .WithOne(scg => scg.StageGrade)
                .HasForeignKey(scg => scg.StageGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                // المرحلة: الروضة
                new StageGrade
                {
                    Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000001"),
                    StageId = Guid.Parse("22222222-2222-2222-2222-222222222783"), // الروضة
                    GradeId = Guid.Parse("44444444-4444-4444-4444-444444444445")  // KG
                },

                // المرحلة الابتدائية (الصف الأول إلى السادس)
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000002"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("11111111-1111-1111-1111-111111111111") }, // الصف الأول
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000003"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000004"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000005"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000006"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("55555555-5555-5555-5555-555555555555") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000007"), StageId = Guid.Parse("11111111-1111-1111-1111-111111111112"), GradeId = Guid.Parse("66666666-6666-6666-6666-666666666666") },

                // المرحلة الإعدادية (الصف السابع إلى التاسع)
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000008"), StageId = Guid.Parse("22222222-2222-2222-2222-222222222223"), GradeId = Guid.Parse("77777777-7777-7777-7777-777777777777") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000009"), StageId = Guid.Parse("22222222-2222-2222-2222-222222222223"), GradeId = Guid.Parse("88888888-8888-8888-8888-888888888888") },
                new StageGrade { Id = Guid.Parse("aaaa1111-0000-0000-0000-000000000010"), StageId = Guid.Parse("22222222-2222-2222-2222-222222222223"), GradeId = Guid.Parse("99999999-9999-9999-9999-999999999999") }
            );
        }
    }
}
