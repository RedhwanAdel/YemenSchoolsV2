using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			builder.HasKey(t => t.Id);

			builder.ToTable("Teachers");

			builder.Property(t => t.NameAr)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.NameEn)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.Email)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(t => t.PhoneNumber)
				.HasMaxLength(20);

			builder.Property(t => t.Address)
				.HasMaxLength(250);

			builder.Property(t => t.Specialization)
				.HasMaxLength(100);

			builder.Property(t => t.EmploymentStatus)
				.HasMaxLength(50);

			builder.Property(t => t.ProfilePictureUrl)
				.HasMaxLength(300);

			// تعيين القيمة الافتراضية لحقل `HireDate`
			builder.Property(t => t.HireDate)
				.HasDefaultValueSql("GETUTCDATE()");

			// تعيين النوع `Gender` كسلسلة نصية وحفظه كـ `string` في قاعدة البيانات
			builder.Property(t => t.Gender)
				.HasConversion<string>()
				.IsRequired();

			// العلاقة مع `AssignedSubject` (One-to-Many)
			builder.HasMany(t => t.AssignedSubjects)
				.WithOne(a => a.Teacher)
				.HasForeignKey(a => a.TeacherId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(t => t.School)  // مع المدرسة
	   .WithMany(s => s.Teachers)  // العديد من المعلمين يمكن أن يكونوا في نفس المدرسة
	   .HasForeignKey(t => t.SchoolId)  // ارتباط المعلم بالمدرسة باستخدام SchoolId
	   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}