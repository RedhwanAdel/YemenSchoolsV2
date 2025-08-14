using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Dto.Attendance
{
    public class CreateDailyAttendanceDto
    {
        public Guid SectionId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<Guid, AttendanceStatus> StudentStatuses { get; set; } = new();
    }
}
