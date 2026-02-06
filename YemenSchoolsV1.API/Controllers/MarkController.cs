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

        [HttpGet("StudentSubjectsReport/{studentId:guid}")]
        public async Task<IActionResult> GetStudentSubjectsReport(Guid studentId)
        {
            var response = await Mediator.Send(new GetStudentSubjectsReportQuery { StudentId = studentId });
            return NewResult(response);
        }

        [HttpGet("section-subjects")]
        public async Task<IActionResult> GetTeacherSectionSubjects()
        {
            var teacherId = User.GetEntityId();
            var response = await Mediator.Send(new GetTeacherSectionSubjectsQuery { TeacherId = teacherId });
            return NewResult(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMarks([FromBody] CreateMarksDto dto)
        {
            var teacherId = User.GetEntityId();
            var response = await Mediator.Send(new CreateMarksCommand
            {
                TeacherId = teacherId,
                SectionSubjectId = dto.SectionSubjectId,
                AssessmentType = dto.AssessmentType,
                StudentScores = dto.StudentScores,
                MaxScore = dto.MaxScore
            });
            return NewResult(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMarks([FromBody] UpdateMarksDto dto)
        {
            var teacherId = User.GetEntityId();
            var response = await Mediator.Send(new UpdateMarksCommand
            {
                TeacherId = teacherId,
                SectionSubjectId = dto.SectionSubjectId,
                AssessmentType = dto.AssessmentType,
                StudentScores = dto.StudentScores
            });
            return NewResult(response);
        }

        [HttpGet("student-transcript/{studentId:guid}")]
        public async Task<IActionResult> GetStudentTranscript(Guid studentId)
        {
            var response = await Mediator.Send(new GetStudentTranscriptQuery { StudentId = studentId });
            return NewResult(response);
        }

        [HttpGet("section-report/{sectionSubjectId:guid}")]
        public async Task<IActionResult> GetSectionMarkReport(Guid sectionSubjectId)
        {
            var response = await Mediator.Send(new GetSectionMarkReportQuery { SectionSubjectId = sectionSubjectId });
            return NewResult(response);
        }
    }
}
