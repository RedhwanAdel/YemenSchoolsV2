using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.DailyLogs.Commands.CreateDailyLog;
using YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetDailyLogById;
using YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogs;
using YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogsForDay;

namespace YemenSchoolsV1.API.Controllers
{
    public class DailyLogController : AppControllerBase
    {

        [HttpGet("student/{studentId}/daily")]
        public async Task<IActionResult> GetStudentDailyLogsForDay(Guid studentId, [FromQuery] DateTime date)
        {
            var response = await Mediator.Send(new GetStudentDailyLogsForDayQuery(studentId, date));
            return Ok(response);
        }

        // ------------------- إضافة سجل يومي (للمعلم) -------------------
        [HttpPost]
        public async Task<IActionResult> CreateDailyLog([FromBody] DailyLogCreateDto dto)
        {
            var teacherId = User.GetEntityId();

            var command = new CreateDailyLogCommand
            {
                LessonCovered = dto.LessonCovered,
                HomeworkAssigned = dto.HomeworkAssigned,
                TeacherNotes = dto.TeacherNotes,
                SectionSubjectId = dto.SectionSubjectId,
                TeacherId = teacherId
            };

            var response = await Mediator.Send(command);

            if (response.Succeeded)
            {
                return CreatedAtAction(nameof(GetDailyLog), new { id = response.Data.Id }, response.Data);
            }
            return NewResult(response);
        }

        // ------------------- جلب سجل معين (للمشاهدة) -------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyLog(Guid id)
        {
            var response = await Mediator.Send(new GetDailyLogByIdQuery(id));
            return NewResult(response);
        }

        // ------------------- جلب سجلات طالب (لولي الأمر والطالب) -------------------
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentDailyLogs(Guid studentId)
        {
            var response = await Mediator.Send(new GetStudentDailyLogsQuery(studentId));
            return NewResult(response);
        }
    }
}
