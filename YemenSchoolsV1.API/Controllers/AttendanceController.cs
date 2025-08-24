using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Attendance;
using YemenSchoolsV1.Application.Extensions;

namespace YemenSchoolsV1.API.Controllers
{
    public class AttendanceController : AppControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Creates daily attendance for a section.
        /// </summary>
        [HttpPost("daily")]
        public async Task<IActionResult> CreateDailyAttendance([FromBody] CreateDailyAttendanceDto dto)
        {
            var classTeacherId = User.GetEntityId(); // افتراض أن لديك دالة تجلب Id من JWT Token مثلاً

            if (dto == null || dto.StudentStatuses == null || !dto.StudentStatuses.Any())
                return NewResult<object>(new Response<object>("Invalid attendance data.", false) { StatusCode = HttpStatusCode.BadRequest });

            var attendance = await _attendanceService.CreateDailyAttendanceAsync(
             classTeacherId, dto.SectionId, dto.Date, dto.StudentStatuses);

            // Return only success message and created attendance ID
            return NewResult(new Response<Guid>(attendance.Id, "Attendance created successfully") { StatusCode = HttpStatusCode.Created, Succeeded = true });
        }

        /// <summary>
        /// Updates daily attendance for a section.
        /// </summary>
        [HttpPut("daily")]
        public async Task<IActionResult> UpdateDailyAttendance([FromBody] UpdateDailyAttendanceDto dto)
        {
            if (dto == null || dto.NewStudentStatuses == null || !dto.NewStudentStatuses.Any())
                return NewResult<object>(new Response<object>("Invalid update data.", false) { StatusCode = HttpStatusCode.BadRequest });

            await _attendanceService.UpdateDailyAttendanceAsync(dto.AttendanceId, dto.NewStudentStatuses);
            return NewResult<object>(new Response<object>("Attendance updated successfully.", true) { StatusCode = HttpStatusCode.OK });
        }

        /// <summary>
        /// Gets the attendance report for a student.
        /// </summary>
        [HttpGet("student/{studentId}/report")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NewResult<object>(new Response<object>("Invalid student ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var details = await _attendanceService.GetStudentAttendanceReportAsync(studentId);
            var result = details.Select(d => new AttendanceDetailDto
            {
                Id = d.Id,
                AttendanceId = d.AttendanceId,
                StudentId = d.StudentId,
                Status = d.Status,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt
            }).ToList();

            // Explicitly specify the type argument for NewResult
            return NewResult(new Response<List<AttendanceDetailDto>>(result, "Attendance report retrieved successfully") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpGet("student/{studentId}/report/{year}/{month}")]
        public async Task<IActionResult> GetStudentAttendanceReport(Guid studentId, [FromRoute] int year, [FromRoute] int month)
        {
            if (studentId == Guid.Empty)
                return NewResult<object>(new Response<object>("Invalid student ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            // نفس التحقق من صحة الشهر والسنة
            if (year < 2000 || month < 1 || month > 12)
                return NewResult<object>(new Response<object>("Invalid year or month.", false) { StatusCode = HttpStatusCode.BadRequest });

            // استدعاء الدالة الجديدة في الخدمة مع تمرير المعلمات
            var details = await _attendanceService.GetStudentAttendanceByMonthAsync(studentId, year, month);

            var result = details.Select(d => new AttendanceDetailDto
            {
                Id = d.Id,
                AttendanceId = d.AttendanceId,
                StudentId = d.StudentId,
                Status = d.Status,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt
            }).ToList();

            return NewResult(new Response<List<AttendanceDetailDto>>(result, "Attendance report retrieved successfully") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }
    }
}
