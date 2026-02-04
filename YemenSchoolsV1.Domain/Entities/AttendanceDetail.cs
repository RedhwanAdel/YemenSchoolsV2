using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
    public class AttendanceDetail
    {
        public Guid Id { get; set; }
        public Guid AttendanceId { get; set; } // المفتاح الخارجي لجدول Attendance
        public Guid StudentId { get; set; }

        // يمكن أن يكون enum
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Attendance Attendance { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
