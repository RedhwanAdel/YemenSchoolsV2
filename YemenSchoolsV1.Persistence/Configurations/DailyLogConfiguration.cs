using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class DailyLogConfiguration : IEntityTypeConfiguration<DailyLog>
    {
        public void Configure(EntityTypeBuilder<DailyLog> builder)
        {

            // المفتاح الأساسي
            builder.HasKey(dl => dl.Id);

            // الحقول
            builder.Property(dl => dl.LessonCovered)
                .HasMaxLength(500); // نص الدرس (اختياري)

            builder.Property(dl => dl.HomeworkAssigned)
                .HasMaxLength(500); // الواجب (اختياري)

            builder.Property(dl => dl.TeacherNotes)
                .HasMaxLength(1000); // ملاحظات المعلم

            builder.Property(dl => dl.Date)
                .IsRequired();

            // العلاقات
            builder.HasOne(dl => dl.SectionSubject)
                .WithMany(t => t.DailyLogs)
                .HasForeignKey(dl => dl.SectionSubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dl => dl.Teacher)
                .WithMany(t => t.DailyLogs)
                .HasForeignKey(dl => dl.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // فهرس لتحسين الاستعلامات (مفيد جدًا)
            builder.HasIndex(dl => new { dl.SectionSubjectId, dl.Date });
        }
    }
}
