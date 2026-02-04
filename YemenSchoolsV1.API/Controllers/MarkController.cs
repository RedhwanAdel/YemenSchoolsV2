using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Marks.Commands.CreateMarks;
using YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks;
using YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport;
using YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentSubjectsReport;
using YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript;
using YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects;

namespace YemenSchoolsV1.API.Controllers
{
    public class MarkController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public MarkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Marks/StudentSubjectsReport/{studentId}
        [HttpGet("StudentSubjectsReport/{studentId}")]
        public async Task<ActionResult<IEnumerable<StudentSubjectReportDto>>> GetStudentSubjectsReport(Guid studentId)
        {
            var query = new GetStudentSubjectsReportQuery { StudentId = studentId };
            var report = await _mediator.Send(query);

            return Ok(report);
        }

        /// <summary>
        /// لجلب جميع الشعب والمواد التي يدرسها المعلم الحالي
        /// </summary>
        [HttpGet("section-subjects")]
        public async Task<IActionResult> GetTeacherSectionSubjects()
        {
            try
            {
                // استخراج معرف المعلم من المصادقة
                var teacherId = User.GetEntityId();
                var query = new GetTeacherSectionSubjectsQuery { TeacherId = teacherId };
                var sectionSubjects = await _mediator.Send(query);
                return Ok(sectionSubjects);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving teacher's subjects and sections.");
            }
        }

        /// <summary>
        /// لإنشاء درجات جديدة لمجموعة من الطلاب
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateMarks([FromBody] CreateMarksDto dto)
        {
            try
            {
                var teacherId = User.GetEntityId(); // استخراج معرف المعلم من المصادقة
                var command = new CreateMarksCommand
                {
                    TeacherId = teacherId,
                    SectionSubjectId = dto.SectionSubjectId,
                    AssessmentType = dto.AssessmentType,
                    StudentScores = dto.StudentScores,
                    MaxScore = dto.MaxScore
                };

                var result = await _mediator.Send(command);

                if (!result.Succeeded)
                {
                    return BadRequest(new { message = result.Message });
                }

                return StatusCode(201, new { message = result.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the marks.");
            }
        }

        /// <summary>
        /// لتحديث درجات موجودة لمجموعة من الطلاب
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMarks([FromBody] UpdateMarksDto dto)
        {
            try
            {
                var teacherId = User.GetEntityId();
                var command = new UpdateMarksCommand
                {
                    TeacherId = teacherId,
                    SectionSubjectId = dto.SectionSubjectId,
                    AssessmentType = dto.AssessmentType,
                    StudentScores = dto.StudentScores
                };

                var result = await _mediator.Send(command);

                if (!result.Succeeded)
                {
                    return BadRequest(new { message = result.Message });
                }

                return Ok(new { message = result.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the marks.");
            }
        }

        /// <summary>
        /// لجلب كشف درجات طالب واحد
        /// </summary>
        [HttpGet("student-transcript/{studentId}")]
        public async Task<IActionResult> GetStudentTranscript(Guid studentId)
        {
            try
            {
                var query = new GetStudentTranscriptQuery { StudentId = studentId };
                var transcript = await _mediator.Send(query);
                return Ok(transcript);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Student not found." });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving student transcript.");
            }
        }

        /// <summary>
        /// لجلب تقرير شامل لدرجات شعبة في مادة معينة
        /// </summary>
        [HttpGet("section-report/{sectionSubjectId}")]
        public async Task<IActionResult> GetSectionMarkReport(Guid sectionSubjectId)
        {
            try
            {
                var query = new GetSectionMarkReportQuery { SectionSubjectId = sectionSubjectId };
                var report = await _mediator.Send(query);
                return Ok(report);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Section or Subject not found." });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the section report.");
            }
        }
    }
}
