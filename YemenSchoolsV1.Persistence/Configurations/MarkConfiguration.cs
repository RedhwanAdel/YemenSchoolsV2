using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class MarkConfiguration : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            // تحديد المفتاح الأساسي
            builder.HasKey(m => m.Id);

            // العلاقة مع Student (متعدد إلى واحد)
            builder.HasOne(m => m.Student)
                   .WithMany(s => s.Marks)
                   .HasForeignKey(m => m.StudentId)
                   .OnDelete(DeleteBehavior.Cascade); // حذف الدرجات عند حذف الطالب

            // العلاقة مع SectionSubject (متعدد إلى واحد)
            builder.HasOne(m => m.SectionSubject)
                   .WithMany(ss => ss.Marks)
                   .HasForeignKey(m => m.SectionSubjectId)
                   .OnDelete(DeleteBehavior.NoAction); // لا تحذف الدرجات عند حذف العلاقة

            // تعريف نوع التقييم AssessmentType كحقل مطلوب
            builder.Property(m => m.AssessmentType)
                   .IsRequired();
        }
    }
}
