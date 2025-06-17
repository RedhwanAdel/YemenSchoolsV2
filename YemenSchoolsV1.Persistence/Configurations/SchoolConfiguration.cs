using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class SchoolConfiguration : IEntityTypeConfiguration<School>
	{
		public void Configure(EntityTypeBuilder<School> builder)
		{
			// تعيين المفتاح الأساسي
			builder.HasKey(s => s.Id);

			// تعريف الحقول المطلوبة
			builder.Property(s => s.NameAr).IsRequired().HasMaxLength(100);
			builder.Property(s => s.NameEn).IsRequired().HasMaxLength(100);

			// الحقول الاختيارية
			builder.Property(s => s.DescriptionAr).HasMaxLength(500);
			builder.Property(s => s.DescriptionEn).HasMaxLength(500);
			builder.Property(s => s.AddressAr).IsRequired().HasMaxLength(200);
			builder.Property(s => s.AddressEn).IsRequired().HasMaxLength(200);

			// إعدادات العلاقات
			builder.HasOne(s => s.City)
				   .WithMany(c => c.Schools)
				   .HasForeignKey(s => s.CityId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(s => s.Region)
				   .WithMany(r => r.Schools)
				   .HasForeignKey(s => s.RegionId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(s => s.Teachers)  // المدرسة تحتوي على العديد من المعلمين
				   .WithOne(t => t.School)  // المعلم مرتبط بمدرسة واحدة
				   .HasForeignKey(t => t.SchoolId)  // استخدام `SchoolId` في جدول المعلمين
				   .OnDelete(DeleteBehavior.Restrict);
			builder.HasMany(s => s.Subjects)  // المدرسة تحتوي على العديد من المواد
				.WithOne(sub => sub.School)  // المادة مرتبطة بمدرسة واحدة
											 .HasForeignKey(sub => sub.SchoolId)  // استخدام `SchoolId` في جدول المواد
										.OnDelete(DeleteBehavior.Restrict);
		}
	}
}