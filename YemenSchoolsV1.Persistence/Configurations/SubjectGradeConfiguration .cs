using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class SubjectGradeConfiguration : IEntityTypeConfiguration<SubjectGrade>
	{
		public void Configure(EntityTypeBuilder<SubjectGrade> builder)
		{
			builder.HasKey(sg => new { sg.SubjectId, sg.GradeId });

			builder.ToTable("SubjectGrades");

			builder.Property(sg => sg.MinPassMark)
				.IsRequired()
				.HasColumnType("decimal(5,2)"); // تحديد الدقة العددية

			builder.Property(sg => sg.MaxMark)
				.IsRequired()
				.HasColumnType("decimal(5,2)");

			// العلاقة بين `SubjectGrade` و `Subject` (Many-to-One)
			builder.HasOne(sg => sg.Subject)
				.WithMany(s => s.SubjectGrades)
				.HasForeignKey(sg => sg.SubjectId)
				.OnDelete(DeleteBehavior.Restrict); // عند حذف المادة، يتم حذف السجلات المرتبطة بها في `SubjectGrade`

			// العلاقة بين `SubjectGrade` و `Grade` (Many-to-One)
			builder.HasOne(sg => sg.Grade)
				.WithMany(g => g.SubjectGrades)
				.HasForeignKey(sg => sg.GradeId)
				.OnDelete(DeleteBehavior.Restrict); // عند حذف الصف، يتم حذف السجلات المرتبطة به في `SubjectGrade`


		}
	}
}
