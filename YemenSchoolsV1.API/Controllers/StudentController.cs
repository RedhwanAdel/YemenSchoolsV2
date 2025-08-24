using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Students;

namespace YemenSchoolsV1.API.Controllers
{
    public class StudentController : AppControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
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

            var students = await _studentService.GetStudentsByAcademicYearAndSectionAsync(academicYearId, sectionId);

            var result = students.Select(s => new StudentListDto
            {
                Id = s.Id,
                Name = s.NameAr,

                RegisterNo = s.RegisterNo,

            }).ToList();

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
                var response = new Response<List<StudentListDto>>("AcademicYearId and SectionId are required.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return NewResult(response);
            }

            var students = await _studentService.GetStudentsBySectionAsync(sectionId);

            var result = students.Select(s => new StudentListDto
            {
                Id = s.Id,
                Name = s.NameAr,
                SectionId = s.CurrentSectionId,
                RegisterNo = s.RegisterNo,

            }).ToList();

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


            var students = await _studentService.GetStudentsBySchoolIdAsync(schoolId);


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
                _logger.LogWarning("Invalid model state for CreateStudent request. DTO: {@Dto}", dto);
                return BadRequest(ModelState);
            }

            var (succeeded, message) = await _studentService.CreateStudentAsync(dto);

            _logger.LogInformation("CreateStudent result: {Succeeded}, Message: {Message}", succeeded, message);

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
            var student = await _studentService.GetStudentProfileWithParentsAsync(id);

            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found.", id);
                var response = new Response<StudentWithParentsDto>("الطالب غير موجود", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return NewResult(response);
            }

            return NewResult(new Response<StudentWithParentsDto>(student, "تم جلب بيانات الطالب بنجاح") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true });
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
                _logger.LogWarning("Invalid model state for UpdateStudentProfile request. DTO: {@Dto}", dto);
                return BadRequest(ModelState);
            }

            var (succeeded, message) = await _studentService.UpdateStudentProfileAsync(id, dto);

            _logger.LogInformation("UpdateStudentProfile result for ID {StudentId}: {Succeeded}, Message: {Message}", id, succeeded, message);

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
            var (succeeded, message) = await _studentService.AddParentToStudentAsync(studentId, parentId, relationType);

            _logger.LogInformation("AddParentToStudent: Parent {ParentId} to Student {StudentId} with Relation {RelationType}. Result: {Succeeded}, Message: {Message}", parentId, studentId, relationType, succeeded, message);

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
            var (succeeded, message) = await _studentService.RemoveParentFromStudentAsync(studentId, parentId);

            _logger.LogInformation("RemoveParentFromStudent: Parent {ParentId} from Student {StudentId}. Result: {Succeeded}, Message: {Message}", parentId, studentId, succeeded, message);

            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            return NewResult(new Response<string>(message, succeeded) { StatusCode = statusCode });

        }

    }
}
