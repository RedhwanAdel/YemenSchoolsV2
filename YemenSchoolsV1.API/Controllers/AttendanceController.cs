using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Dto.Attendance;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Attendance.Commands.CreateDailyAttendance;
using YemenSchoolsV1.Application.Features.Attendance.Commands.UpdateDailyAttendance;
using YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceByMonth;
using YemenSchoolsV1.Application.Features.Attendance.Queries.GetStudentAttendanceReport;

namespace YemenSchoolsV1.API.Controllers
{
    public class AttendanceController : AppControllerBase
    {
        /// <summary>
        /// Creates daily attendance for a section.
        /// </summary>
        [HttpPost("daily")]
        public async Task<IActionResult> CreateDailyAttendance([FromBody] CreateDailyAttendanceDto dto)
        {
            var classTeacherId = User.GetEntityId();

            if (dto == null || dto.StudentStatuses == null || !dto.StudentStatuses.Any())
                return NewResult(new Response<object>("Invalid attendance data.", false) { StatusCode = HttpStatusCode.BadRequest });

            var response = await Mediator.Send(new CreateDailyAttendanceCommand(
                classTeacherId, dto.SectionId, dto.Date, dto.StudentStatuses));

            return NewResult(response);
        }

        /// <summary>
        /// Updates daily attendance for a section.
        /// </summary>
        [HttpPut("daily")]
        public async Task<IActionResult> UpdateDailyAttendance([FromBody] UpdateDailyAttendanceDto dto)
        {
            if (dto == null || dto.NewStudentStatuses == null || !dto.NewStudentStatuses.Any())
                return NewResult(new Response<object>("Invalid update data.", false) { StatusCode = HttpStatusCode.BadRequest });

            var response = await Mediator.Send(new UpdateDailyAttendanceCommand(dto.AttendanceId, dto.NewStudentStatuses));
            return NewResult(response);
        }

        /// <summary>
        /// Gets the attendance report for a student.
        /// </summary>
        [HttpGet("student/{studentId}/report")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NewResult(new Response<object>("Invalid student ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var response = await Mediator.Send(new GetStudentAttendanceReportQuery(studentId));
            return NewResult(response);
        }


        [HttpGet("student/{studentId}/report/{year}/{month}")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId, [FromRoute] int year, [FromRoute] int month)
        {
            if (studentId == Guid.Empty)
                return NewResult(new Response<object>("Invalid student ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            if (year < 2000 || month < 1 || month > 12)
                return NewResult(new Response<object>("Invalid year or month.", false) { StatusCode = HttpStatusCode.BadRequest });

            var response = await Mediator.Send(new GetStudentAttendanceByMonthQuery(studentId, year, month));
            return NewResult(response);
        }
    }
}
