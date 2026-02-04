using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Dto.Attendance
{
    public class UpdateDailyAttendanceDto
    {
        public Guid AttendanceId { get; set; }
        public Dictionary<Guid, AttendanceStatus> NewStudentStatuses { get; set; } = new();
    }
}
