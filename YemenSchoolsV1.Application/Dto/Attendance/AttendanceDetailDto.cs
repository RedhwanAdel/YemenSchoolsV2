using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Dto.Attendance
{
    public class AttendanceDetailDto
    {
        public Guid Id { get; set; }
        public Guid AttendanceId { get; set; }
        public Guid StudentId { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
