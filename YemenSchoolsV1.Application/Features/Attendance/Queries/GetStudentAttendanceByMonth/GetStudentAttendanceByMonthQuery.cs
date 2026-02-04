using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Attendance;

namespace YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceByMonth
{
    public class GetStudentAttendanceByMonthQuery : IRequest<Response<List<AttendanceDetailDto>>>
    {
        public Guid StudentId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public GetStudentAttendanceByMonthQuery(Guid studentId, int year, int month)
        {
            StudentId = studentId;
            Year = year;
            Month = month;
        }
    }
}
