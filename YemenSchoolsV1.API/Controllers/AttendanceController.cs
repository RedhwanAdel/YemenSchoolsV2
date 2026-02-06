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
        [HttpPost("daily")]
        public async Task<IActionResult> CreateDailyAttendance([FromBody] CreateDailyAttendanceDto dto)
        {
            var classTeacherId = User.GetEntityId();
            var response = await Mediator.Send(new CreateDailyAttendanceCommand(
                classTeacherId, dto.SectionId, dto.Date, dto.StudentStatuses));

            return NewResult(response);
        }

        [HttpPut("daily")]
        public async Task<IActionResult> UpdateDailyAttendance([FromBody] UpdateDailyAttendanceDto dto)
        {
            var response = await Mediator.Send(new UpdateDailyAttendanceCommand(dto.AttendanceId, dto.NewStudentStatuses));
            return NewResult(response);
        }

        [HttpGet("student/{studentId:guid}/report")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId)
        {
            var response = await Mediator.Send(new GetStudentAttendanceReportQuery(studentId));
            return NewResult(response);
        }

        [HttpGet("student/{studentId:guid}/report/{year:int}/{month:int}")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId, int year, int month)
        {
            var response = await Mediator.Send(new GetStudentAttendanceByMonthQuery(studentId, year, month));
            return NewResult(response);
        }
    }
}
