using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class StageConfiguration : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(s => s.StageGrades)
                .WithOne(sg => sg.Stage)
                .HasForeignKey(sg => sg.StageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                   new Stage { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Name = "المرحلة الإبتدائية" },
                   new Stage { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), Name = "المرحلة الإعدادية" },
                   new Stage { Id = Guid.Parse("22222222-2222-2222-2222-222222222783"), Name = "الروضة" },
                   new Stage { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), Name = "المرحلة الثانوية" }
            );
        }
    }
}
