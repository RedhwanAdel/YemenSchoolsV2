using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Attendance;

namespace YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceReport
{
    public class GetStudentAttendanceReportQuery : IRequest<Response<List<AttendanceDetailDto>>>
    {
        public Guid StudentId { get; set; }

        public GetStudentAttendanceReportQuery(Guid studentId)
        {
            StudentId = studentId;
        }
    }
}
