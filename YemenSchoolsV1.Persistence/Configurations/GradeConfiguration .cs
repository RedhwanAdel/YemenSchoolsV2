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

			builder.Property(g => g.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(g => g.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// تعيين `IsActive` كحقل افتراضي قيمته `true`
			builder.Property(g => g.IsActive)
				.HasDefaultValue(true);

			// العلاقة مع `term` (Many-to-One)
			builder.HasOne(g => g.Term)
				.WithMany(s => s.Grades)
				.HasForeignKey(g => g.TermId)
				.OnDelete(DeleteBehavior.Restrict);

			// العلاقة مع `Sections` (One-to-Many)
			builder.HasMany(g => g.Sections)
				.WithOne(s => s.Grade)
				.HasForeignKey(s => s.GradeId)
				.OnDelete(DeleteBehavior.Restrict);

			// العلاقة مع `SubjectGrades` (One-to-Many)
			builder.HasMany(g => g.SubjectGrades)
				.WithOne(sg => sg.Grade)
				.HasForeignKey(sg => sg.GradeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}

}
