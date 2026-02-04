using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Attendance.Commands.CreateDailyAttendance
{
    public class CreateDailyAttendanceCommand : IRequest<Response<Guid>>
    {
        public Guid ClassTeacherId { get; set; }
        public Guid SectionId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<Guid, YemenSchoolsV1.Domain.Enums.AttendanceStatus> StudentStatuses { get; set; }

        public CreateDailyAttendanceCommand(Guid classTeacherId, Guid sectionId, DateTime date, Dictionary<Guid, YemenSchoolsV1.Domain.Enums.AttendanceStatus> studentStatuses)
        {
            ClassTeacherId = classTeacherId;
            SectionId = sectionId;
            Date = date;
            StudentStatuses = studentStatuses;
        }
    }
}
