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
        /// ينشئ سجلًا جديدًا لطالب في النظام.
        /// </summary>
        /// <param name="dto">كائن نقل البيانات (DTO) الذي يحتوي على تفاصيل الطالب.</param>
        /// <returns>نتيجة HTTP التي تشير إلى نجاح أو فشل العملية.</returns>
        /// <response code="201">تم إنشاء الطالب بنجاح.</response>
        /// <response code="400">فشل في إنشاء الطالب بسبب بيانات غير صحيحة.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateStudent request.");
                return BadRequest(ModelState);
            }

            var (succeeded, message) = await _studentService.CreateStudentAsync(dto);

            if (succeeded)
            {
                _logger.LogInformation("Student created successfully.");
                return StatusCode(StatusCodes.Status201Created, new { message });
            }

            _logger.LogError("Failed to create student: {Message}", message);
            return BadRequest(new { message });
        }

        /// <summary>
        /// يجلب ملف الطالب مع تفاصيل أولياء أموره.
        /// </summary>
        /// <param name="id">معرف الطالب (GUID).</param>
        /// <returns>ملف الطالب أو لا يوجد.</returns>
        /// <response code="200">يتم إرجاع ملف الطالب.</response>
        /// <response code="404">لم يتم العثور على طالب بالمعرف المحدد.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentProfile(Guid id)
        {
            var student = await _studentService.GetStudentProfileWithParentsAsync(id);

            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found.", id);
                return NotFound(new { message = "الطالب غير موجود." });
            }

            return Ok(student);
        }

        /// <summary>
        /// يقوم بتحديث ملف الطالب.
        /// </summary>
        /// <param name="id">معرف الطالب (GUID).</param>
        /// <param name="dto">كائن نقل البيانات (DTO) للتحديث.</param>
        /// <returns>نتيجة HTTP.</returns>
        /// <response code="200">تم التحديث بنجاح.</response>
        /// <response code="400">فشل التحديث.</response>
        /// <response code="404">الطالب غير موجود.</response>
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

            var (succeeded, message) = await _studentService.UpdateStudentProfileAsync(id, dto);

            if (succeeded)
            {
                _logger.LogInformation("Student profile with ID {StudentId} updated successfully.", id);
                return Ok(new { message });
            }

            if (message.Contains("الطالب غير موجود"))
            {
                return NotFound(new { message });
            }

            _logger.LogError("Failed to update student profile for ID {StudentId}: {Message}", id, message);
            return BadRequest(new { message });
        }

        /// <summary>
        /// يحذف سجل طالب من النظام.
        /// </summary>
        /// <param name="id">معرف الطالب (GUID).</param>
        /// <returns>نتيجة HTTP.</returns>
        /// <response code="200">تم الحذف بنجاح.</response>
        /// <response code="404">لم يتم العثور على طالب بالمعرف المحدد.</response>
        //[HttpDelete("{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteStudent(Guid id)
        //{
        //    var (succeeded, message) = await _studentService.DeleteStudentAsync(id);

        //    if (succeeded)
        //    {
        //        _logger.LogInformation("Student with ID {StudentId} deleted successfully.", id);
        //        return Ok(new { message });
        //    }

        //    return NotFound(new { message });
        //}

        /// <summary>
        /// يضيف ولي أمر إلى طالب موجود.
        /// </summary>
        /// <param name="studentId">معرف الطالب (GUID).</param>
        /// <param name="parentId">معرف ولي الأمر (GUID).</param>
        /// <param name="relationType">نوع العلاقة (مثال: "Father", "Mother").</param>
        /// <returns>نتيجة HTTP.</returns>
        /// <response code="200">تم الربط بنجاح.</response>
        /// <response code="400">فشل الربط.</response>
        [HttpPost("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParentToStudent(Guid studentId, Guid parentId, [FromQuery] string relationType)
        {
            var (succeeded, message) = await _studentService.AddParentToStudentAsync(studentId, parentId, relationType);

            if (succeeded)
            {
                _logger.LogInformation("Parent {ParentId} added to student {StudentId} with relation {RelationType}.", parentId, studentId, relationType);
                return Ok(new { message });
            }

            return BadRequest(new { message });
        }

        /// <summary>
        /// يزيل ولي أمر من طالب.
        /// </summary>
        /// <param name="studentId">معرف الطالب (GUID).</param>
        /// <param name="parentId">معرف ولي الأمر (GUID).</param>
        /// <returns>نتيجة HTTP.</returns>
        /// <response code="200">تم الإزالة بنجاح.</response>
        /// <response code="400">فشل الإزالة.</response>
        [HttpDelete("{studentId:guid}/parents/{parentId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParentFromStudent(Guid studentId, Guid parentId)
        {
            var (succeeded, message) = await _studentService.RemoveParentFromStudentAsync(studentId, parentId);

            if (succeeded)
            {
                _logger.LogInformation("Parent {ParentId} removed from student {StudentId}.", parentId, studentId);
                return Ok(new { message });
            }

            return BadRequest(new { message });
        }

    }
}
