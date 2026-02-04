using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.UpdateDailyAttendance
{
    public class UpdateDailyAttendanceCommand : IRequest<Response<string>>
    {
        public Guid AttendanceId { get; set; }
        public Dictionary<Guid, YemenSchoolsV1.Domain.Enums.AttendanceStatus> NewStudentStatuses { get; set; }

        public UpdateDailyAttendanceCommand(Guid attendanceId, Dictionary<Guid, YemenSchoolsV1.Domain.Enums.AttendanceStatus> newStudentStatuses)
        {
            AttendanceId = attendanceId;
            NewStudentStatuses = newStudentStatuses;
        }
    }
}
