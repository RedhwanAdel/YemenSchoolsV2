using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{

    public class SchoolReviewConfiguration : IEntityTypeConfiguration<SchoolReview>
    {
        public void Configure(EntityTypeBuilder<SchoolReview> builder)
        {
            builder.ToTable("SchoolReviews");

            builder.HasKey(r => r.Id);

            // العلاقة مع المدرسة
            builder.HasOne(r => r.School)
                   .WithMany(s => s.Reviews)
                   .HasForeignKey(r => r.SchoolId)
                   .OnDelete(DeleteBehavior.Cascade);

            // العلاقة مع المستخدم
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // التقييم (إلزامي)
            builder.Property(r => r.Rating)
                   .IsRequired();

            // التعليق (اختياري وبطول محدد)
            builder.Property(r => r.Comment)
                   .HasMaxLength(500);

            // منع التقييم المكرر (مستخدم واحد يقيم مدرسة واحدة فقط)
            builder.HasIndex(r => new { r.SchoolId, r.UserId }).IsUnique();

            // التاريخ الافتراضي
            builder.Property(r => r.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
