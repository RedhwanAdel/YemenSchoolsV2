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
        public async Task<IActionResult> PromoteStudents([FromBody] PromotionDto dto)
        {
            var command = new PromoteStudentsCommand
            {
                StudentIds = dto.StudentIds,
                NewSectionId = dto.NewSectionId
            };
            var (succeeded, message) = await Mediator.Send(command);

            var response = new Response<string>(message, succeeded);

            if (succeeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return NewResult(response);
            }

            if (message.Contains("غير موجودة") || message.Contains("غير موجود"))
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NewResult(response);
            }

            if (message.Contains("لا يمكن ترقية الطلاب"))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return NewResult(response);
            }

            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            return NewResult(response);
        }

        /// <summary>
        /// Gets students by academic year and section.
        /// </summary>
        [HttpGet("by-academic-year-and-section")]
        public async Task<IActionResult> GetStudentsByAcademicYearAndSection(
            [FromQuery] Guid academicYearId,
            [FromQuery] Guid sectionId)
        {
            if (academicYearId == Guid.Empty || sectionId == Guid.Empty)
            {
                var response = new Response<List<StudentListDto>>("AcademicYearId and SectionId are required.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return NewResult(response);
            }

            var query = new GetStudentsByAcademicYearAndSectionQuery { AcademicYearId = academicYearId, SectionId = sectionId };
            var students = await Mediator.Send(query);

            var result = students.ToList(); // Transformation logic moved to Handler/Mapper

            var successResponse = new Response<List<StudentListDto>>(result, "تم جلب الطلاب بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };

            return NewResult(successResponse);
        }

        /// <summary>
        /// Gets students by academic year and section.
        /// </summary>
        [HttpGet("by-section/{sectionId}")]
        public async Task<IActionResult> GetStudentsBySection(Guid sectionId)
        {
            if (sectionId == Guid.Empty)
            {
                var response = new Response<List<StudentListDto>>("SectionId is required.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return NewResult(response);
            }

            var query = new GetStudentsBySectionQuery { SectionId = sectionId };
            var students = await Mediator.Send(query);

            var result = students.ToList();

            var successResponse = new Response<List<StudentListDto>>(result, "تم جلب الطلاب بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };

            return NewResult(successResponse);
        }


        [HttpGet("student-by-school/{schoolId}")]
        public async Task<IActionResult> GetStudentsBySchool(Guid schoolId)
        {
            var query = new GetStudentsBySchoolIdQuery { SchoolId = schoolId };
            var students = await Mediator.Send(query);

            var successResponse = new Response<IEnumerable<StudentListDto>>(students, "تم جلب الطلاب بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };

            return NewResult(successResponse);
        }

        /// <summary>
        /// ينشئ سجلًا جديدًا لطالب في النظام.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateStudentCommand
            {
                 NameAr = dto.NameAr,
                 NameEn = dto.NameEn,
                 Nationality = dto.Nationality,
                 Address = dto.Address,
                 Gender = dto.Gender,
                 DateOfBirth = dto.DateOfBirth,
                 PhoneNumber = dto.PhoneNumber,
                 Email = dto.Email,
                 RegisterNo = dto.RegisterNo,
                 SchoolId = dto.SchoolId,
                 CurrentAcademicYearId = dto.CurrentAcademicYearId,
                 CurrentSectionId = dto.CurrentSectionId,
                 Parents = dto.Parents
            };

            var (succeeded, message) = await Mediator.Send(command);

            if (succeeded)
            {
                return NewResult(new Response<string>(message, true) { StatusCode = System.Net.HttpStatusCode.Created, Succeeded = true });
            };

            var response = new Response<string>(message, succeeded)
            {
                StatusCode = succeeded ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.BadRequest
            };

            return NewResult(response);
        }

        /// <summary>
        /// يجلب ملف الطالب مع تفاصيل أولياء أموره.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentProfile(Guid id)
        {
            try
            {
                var query = new GetStudentProfileWithParentsQuery { StudentId = id };
                var student = await Mediator.Send(query);
                
                return NewResult(new Response<StudentWithParentsDto>(student, "تم جلب بيانات الطالب بنجاح") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true });
            }
            catch (KeyNotFoundException)
            {
                 var response = new Response<StudentWithParentsDto>("الطالب غير موجود", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return NewResult(response);
            }
        }

        /// <summary>
        /// يقوم بتحديث ملف الطالب.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudentProfile(Guid id, [FromBody] StudentUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateStudentProfileCommand
            {
                StudentId = id,
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                Nationality = dto.Nationality,
                Address = dto.Address,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email
            };

            var (succeeded, message) = await Mediator.Send(command);

            if (succeeded)
            {
                return NewResult(new Response<string>(message, true) { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true });
            }

            if (message.Contains("الطالب غير موجود"))
            {
                var response = new Response<string>(message, false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return NewResult(response);
            }

            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            return NewResult(new Response<string>(message, succeeded) { StatusCode = statusCode });
        }



        /// <summary>
        /// يضيف ولي أمر إلى طالب موجود.
        /// </summary>
        [HttpPost("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParentToStudent(Guid studentId, Guid parentId, [FromQuery] string relationType)
        {
            var command = new AddParentToStudentCommand { StudentId = studentId, ParentId = parentId, RelationType = relationType };
            var (succeeded, message) = await Mediator.Send(command);

            if (succeeded) return NewResult(new Response<string>("تم إضافة ولي الأمر بنجاح", true) { StatusCode = System.Net.HttpStatusCode.OK });
            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            return NewResult(new Response<string>(message, succeeded) { StatusCode = statusCode });
        }

        /// <summary>
        /// يزيل ولي أمر من طالب.
        /// </summary>
        [HttpDelete("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParentFromStudent(Guid studentId, Guid parentId)
        {
            var command = new RemoveParentFromStudentCommand { StudentId = studentId, ParentId = parentId };
            var (succeeded, message) = await Mediator.Send(command);

            if (succeeded) return NewResult(new Response<string>("تم إزالة ولي الأمر بنجاح", true) { StatusCode = System.Net.HttpStatusCode.OK });
            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            return NewResult(new Response<string>(message, succeeded) { StatusCode = statusCode });

        }

    }
}
