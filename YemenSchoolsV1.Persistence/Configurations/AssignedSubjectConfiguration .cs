using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class AssignedSubjectConfiguration : IEntityTypeConfiguration<AssignedSubject>
	{
		public void Configure(EntityTypeBuilder<AssignedSubject> builder)
		{
			builder.HasKey(sg => new { sg.SubjectId, sg.SectionId, sg.TeacherId });

			builder.ToTable("AssignedSubjects");

			// إعداد العلاقة مع Teacher (كل AssignedSubject مرتبط بـ Teacher واحد)
			builder.HasOne(a => a.Teacher)
				.WithMany(t => t.AssignedSubjects)
				.HasForeignKey(a => a.TeacherId)
				.OnDelete(DeleteBehavior.Restrict); // منع الحذف التتابعي

			// إعداد العلاقة مع Subject (كل AssignedSubject مرتبط بـ  واحد)
			builder.HasOne(a => a.Subject)
				.WithMany(sg => sg.AssignedSubjects)
				.HasForeignKey(a => a.SubjectId)
				.OnDelete(DeleteBehavior.Restrict); // حذف كل AssignedSubjects عند حذف SubjectGrade

			// إعداد العلاقة مع Section (كل AssignedSubject مرتبط بـ Section واحد)
			builder.HasOne(a => a.Section)
				.WithMany(s => s.AssignedSubjects)
				.HasForeignKey(a => a.SectionId)
				.OnDelete(DeleteBehavior.Restrict); // حذف كل AssignedSubjects عند حذف Section

			// إعداد الحقل AssignedDate
			builder.Property(a => a.AssignedDate)
				.IsRequired()
				.HasColumnType("datetime2"); // تخزين البيانات بتنسيق دقيق للوقت
		}
	}

}
