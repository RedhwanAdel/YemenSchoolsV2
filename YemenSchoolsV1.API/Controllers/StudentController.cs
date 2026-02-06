using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Bases;

using YemenSchoolsV1.Application.Features.Students.Commands.AddParentToStudent;
using YemenSchoolsV1.Application.Features.Students.Commands.CreateStudent;
using YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents;
using YemenSchoolsV1.Application.Features.Students.Commands.RemoveParentFromStudent;
using YemenSchoolsV1.Application.Features.Students.Commands.UpdateStudentProfile;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentProfileWithParents;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsByAcademicYearAndSection;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySection;

namespace YemenSchoolsV1.API.Controllers
{
    public class StudentController : AppControllerBase
    {
        public StudentController()
        {
        }

        [HttpPost("promote")]
        public async Task<IActionResult> PromoteStudents([FromBody] PromoteStudentsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("by-academic-year-and-section")]
        public async Task<IActionResult> GetStudentsByAcademicYearAndSection(
            [FromQuery] GetStudentsByAcademicYearAndSectionQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpGet("by-section/{sectionId:guid}")]
        public async Task<IActionResult> GetStudentsBySection([FromRoute] GetStudentsBySectionQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }


        [HttpGet("student-by-school/{schoolId:guid}")]
        public async Task<IActionResult> GetStudentsBySchool([FromRoute] GetStudentsBySchoolIdQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        /// <summary>
        /// ينشئ سجلًا جديدًا لطالب في النظام.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// يجلب ملف الطالب مع تفاصيل أولياء أموره.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentProfile(Guid id)
        {
            var response = await Mediator.Send(new GetStudentProfileWithParentsQuery { StudentId = id });
            return NewResult(response);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudentProfile(Guid id, [FromBody] UpdateStudentProfileCommand command)
        {
            command.StudentId = id;
            var response = await Mediator.Send(command);
            return NewResult(response);
        }



        [HttpPost("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParentToStudent(Guid studentId, Guid parentId, [FromQuery] string relationType)
        {
            var command = new AddParentToStudentCommand { StudentId = studentId, ParentId = parentId, RelationType = relationType };
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParentFromStudent(Guid studentId, Guid parentId)
        {
            var command = new RemoveParentFromStudentCommand { StudentId = studentId, ParentId = parentId };
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
