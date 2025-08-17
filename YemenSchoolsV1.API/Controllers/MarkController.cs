using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Marks;
using YemenSchoolsV1.Application.Extensions;

namespace YemenSchoolsV1.API.Controllers
{
    public class MarkController : AppControllerBase
    {
        private readonly IMarkService _markService;

        public MarkController(IMarkService markService)
        {
            _markService = markService;
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
                var sectionSubjects = await _markService.GetTeacherSectionSubjectsAsync(teacherId);
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
                await _markService.CreateMarksAsync(
                    teacherId,
                    dto.SectionSubjectId,
                    dto.AssessmentType,
                    dto.StudentScores
                );
                return StatusCode(201, new { message = "Marks created successfully." });
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
                await _markService.UpdateMarksAsync(
                    teacherId,
                    dto.SectionSubjectId,
                    dto.AssessmentType,
                    dto.StudentScores
                );
                return Ok(new { message = "Marks updated successfully." });
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
                var transcript = await _markService.GetStudentTranscriptAsync(studentId);
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
                var report = await _markService.GetSectionMarkReportAsync(sectionSubjectId);
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
