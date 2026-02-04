using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);

            builder.ToTable("Grades");

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(g => g.StageGrades)
                  .WithOne(sg => sg.Grade)
                  .HasForeignKey(sg => sg.GradeId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
                 .HasMany(g => g.StageGrades)
               .WithOne(sg => sg.Grade)
               .HasForeignKey(sg => sg.GradeId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
        new Grade { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "الصف الأول" },
         new Grade { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "الصف الثاني" },
         new Grade { Id = Guid.Parse("44444444-4444-4444-4444-444444444445"), Name = "KG" },
        new Grade { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "الصف الثالث" },
        new Grade { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "الصف الرابع" },
        new Grade { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "الصف الخامس" },
        new Grade { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "الصف السادس" },
        new Grade { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "الصف السابع" },
        new Grade { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "الصف الثامن" },
        new Grade { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "الصف التاسع" }
    );


        }
    }

}
