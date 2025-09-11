using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class DailyLogController : AppControllerBase
    {
        private readonly IDailyLogRepository _repository;

        public DailyLogController(IDailyLogRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("student/{studentId}/daily")]
        public async Task<IActionResult> GetStudentDailyLogsForDay(Guid studentId, [FromQuery] DateTime date)
        {
            var logs = await _repository.GetStudentDailyLogsForDayAsync(studentId, date);

            var dtos = logs.Select(dailyLog => new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SubjectName = dailyLog.SectionSubject.GradeSubject.Subject.Name,
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            }).ToList();

            return Ok(dtos);
        }

        // ------------------- إضافة سجل يومي (للمعلم) -------------------
        [HttpPost]
        public async Task<IActionResult> CreateDailyLog([FromBody] DailyLogCreateDto dto)
        {
            var teacherId = User.GetEntityId();
            // التخطيط اليدوي من DTO إلى Entity
            var dailyLog = new DailyLog
            {
                Id = Guid.NewGuid(),
                LessonCovered = dto.LessonCovered,
                HomeworkAssigned = dto.HomeworkAssigned,
                TeacherNotes = dto.TeacherNotes,
                Date = DateTime.UtcNow,
                SectionSubjectId = dto.SectionSubjectId,
                TeacherId = teacherId
            };

            await _repository.AddDailyLogAsync(dailyLog);

            // التخطيط اليدوي من Entity إلى DTO للإرجاع
            var logToReturn = new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered,
                HomeworkAssigned = dailyLog.HomeworkAssigned,
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            };

            return CreatedAtAction(nameof(GetDailyLog), new { id = logToReturn.Id }, logToReturn);
        }

        // ------------------- جلب سجل معين (للمشاهدة) -------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyLog(Guid id)
        {
            var dailyLog = await _repository.GetDailyLogByIdAsync(id);
            if (dailyLog == null)
            {
                return NotFound();
            }

            // التخطيط اليدوي من Entity إلى DTO
            var dto = new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            };

            return Ok(dto);
        }

        // ------------------- جلب سجلات طالب (لولي الأمر والطالب) -------------------
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentDailyLogs(Guid studentId)
        {
            var logs = await _repository.GetStudentDailyLogsAsync(studentId);

            // التخطيط اليدوي للمجموعة
            var dtos = logs.Select(dailyLog => new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            }).ToList();

            return Ok(dtos);
        }
    }
}
