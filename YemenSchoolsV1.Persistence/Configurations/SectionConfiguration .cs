using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class SectionConfiguration : IEntityTypeConfiguration<Section>
	{
		public void Configure(EntityTypeBuilder<Section> builder)
		{
			builder.HasKey(s => s.Id);

			builder.ToTable("Sections");

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.RoomNumber)
				.IsRequired(false);

			builder.Property(s => s.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(s => s.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// تعيين `IsActive` كحقل افتراضي قيمته `true`
			builder.Property(s => s.IsActive)
				.HasDefaultValue(true);

			// العلاقة مع `Grade` (Many-to-One)
			builder.HasOne(s => s.Grade)
				.WithMany(g => g.Sections)
				.HasForeignKey(s => s.GradeId)
				.OnDelete(DeleteBehavior.Restrict); // عند حذف الصف، يتم حذف جميع الشعب المرتبطة به

			// العلاقة مع `AssignedSubjects` (One-to-Many)
			builder.HasMany(s => s.AssignedSubjects)
				.WithOne(a => a.Section)
				.HasForeignKey(a => a.SectionId)
				.OnDelete(DeleteBehavior.Restrict); // عند حذف الشعبة، يتم حذف جميع المواد المعينة لها
		}
	}
}
