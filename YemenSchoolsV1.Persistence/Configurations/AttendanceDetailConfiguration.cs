using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class AttendanceDetailConfiguration : IEntityTypeConfiguration<AttendanceDetail>
    {
        public void Configure(EntityTypeBuilder<AttendanceDetail> builder)
        {
            // العلاقة مع جدول Attendance
            builder.HasOne(ad => ad.Attendance)
                   .WithMany(a => a.AttendanceDetails)
                   .HasForeignKey(ad => ad.AttendanceId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade); // عند حذف سجل حضور، يتم حذف جميع تفاصيله

            // العلاقة مع جدول Student
            builder.HasOne(ad => ad.Student)
                   .WithMany(s => s.AttendanceDetails) // يجب إضافة Navigation Property في Student
                   .HasForeignKey(ad => ad.StudentId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict); // لا يمكن حذف طالب لديه سجلات حضور
        }
    }
}
