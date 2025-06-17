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

			builder.Property(s => s.NameAr)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.NameEn)
				.IsRequired()
				.HasMaxLength(100);

			// إعداد الحقول الزمنية
			builder.Property(s => s.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(s => s.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAddOrUpdate();

			// إعداد العلاقة مع `SubjectGrade` (One-to-Many)
			builder.HasMany(s => s.SubjectGrades)
				.WithOne(sg => sg.Subject)
				.HasForeignKey(sg => sg.SubjectId)
				.OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(s => s.School)  // مع المدرسة
	   .WithMany(sc => sc.Subjects)  // العديد من المواد يمكن أن تكون في نفس المدرسة
	   .HasForeignKey(s => s.SchoolId)  // ارتباط المادة بالمدرسة باستخدام SchoolId
	   .OnDelete(DeleteBehavior.Restrict);


			// العلاقة بين `Subject` و `AssignedSubjects` (One-to-Many)
			builder.HasMany(sg => sg.AssignedSubjects)
				.WithOne(a => a.Subject)
				.HasForeignKey(a => a.SubjectId)
				.OnDelete(DeleteBehavior.Restrict); // عند حذف `SubjectGrade`، يتم حذف جميع `AssignedSubjects` المرتبطة به
		}
	}
}