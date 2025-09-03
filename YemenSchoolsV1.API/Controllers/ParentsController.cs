using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Dto.Students;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class ParentsController : AppControllerBase
    {

        private readonly IParentService _parentService;
        private readonly ILogger<ParentsController> _logger;

        public ParentsController(IParentService parentService, ILogger<ParentsController> logger)
        {
            _parentService = parentService;
            _logger = logger;
        }



        /// <summary>
        /// Retrieves all teachers who teach the children of the authenticated parent.
        /// </summary>
        /// <returns>List of teachers with related info.</returns>
        [HttpGet("teachers")]
        [ProducesResponseType(typeof(Response<List<TeacherInfoForParentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersForParent()
        {
            var parentId = User.GetEntityId();


            var teachers = await _parentService.GetTeachersForParentAsync(parentId);
            var responseObj = new Response<List<TeacherInfoForParentDto>>(teachers, "تم جلب بيانات المعلمين بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(responseObj);
        }

        /// <summary>
        /// Retrieves all students for a parent, including school, class, section, name, and image.
        /// </summary>
        /// <param name="parentId">The parent's ID (GUID).</param>
        /// <returns>List of students with school info.</returns>
        [HttpGet("{parentId:guid}/students-with-school-info")]
        [ProducesResponseType(typeof(Response<List<StudentWithSchoolInfoDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsWithSchoolInfo(Guid parentId)
        {
            var students = await _parentService.GetStudentsWithSchoolInfoByParentIdAsync(parentId);
            var response = new Response<List<StudentWithSchoolInfoDto>>(students, "تم جلب بيانات الطلاب بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(response);
        }
        /// <summary>
        /// Retrieves a parent's profile and their students by parent ID.
        /// </summary>
        /// <param name="parentId">The parent's ID (GUID).</param>
        /// <returns>Parent profile with students.</returns>
        [HttpGet("{parentId:guid}/with-students")]
        [ProducesResponseType(typeof(Response<ParentWithStudentsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParentWithStudents(Guid parentId)
        {
            var parentDto = await _parentService.GetParentWithStudentsAsync(parentId);

            if (parentDto == null)
            {
                var response = new Response<string>("ولي الأمر غير موجود.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return NewResult(response);
            }

            var result = new Response<ParentWithStudentsDto>(parentDto, "تم جلب بيانات ولي الأمر بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(result);
        }

        [HttpGet("check-national-id/{nationalId}")]
        [ProducesResponseType(typeof(Response<ParentCheckDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckParentByNationalId(string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
            {
                var response = new Response<ParentCheckDto>("رقم الهوية مطلوب", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return NewResult(response);
            }

            var result = await _parentService.CheckParentByNationalIdAsync(nationalId);

            var responseDto = new Response<ParentCheckDto>(
                result,
                result.Exists ? "ولي الأمر موجود." : "ولي الأمر غير موجود."
            )
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };

            return NewResult(responseDto);
        }
        /// <summary>
        /// Checks if a parent exists by National ID.
        /// </summary>
        /// <param name="nationalId">The parent's National ID.</param>
        /// <returns>True if exists, false otherwise.</returns>
        [HttpGet("exist")]
        public async Task<IActionResult> IsParentExist([FromQuery] string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
            {
                var response = new Response<bool>("رقم الهوية مطلوب", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return NewResult(response);
            }

            var exists = await _parentService.IsParentExistByNationalIdAsync(nationalId);
            var result = new Response<bool>(exists, exists ? "ولي الأمر موجود." : "ولي الأمر غير موجود.")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(result);
        }

        /// <summary>
        /// Creates a new parent and an associated user account.
        /// هذا الإجراء مُصمَّم للاستخدام من قبل مسؤول النظام (Admin) أو من خلال عملية تسجيل.
        /// </summary>
        /// <param name="dto">Parent creation data.</param>
        /// <returns>A status result indicating success or failure.</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] ParentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var response = new Response<string>("البيانات المدخلة غير صحيحة", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorsBag = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    )
                };
                return NewResult(response);
            }
            // old password DefaultPassword123!
            string defaultPassword = "Pa$$w0rd";
            var (succeeded, message, parentId) = await _parentService.CreateParentWithUserAsync(dto, defaultPassword);

            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            var responseResult = new Response<object>(
                new { message, parentId },
                message
            )
            {
                StatusCode = statusCode,
                Succeeded = succeeded
            };

            if (!succeeded)
                _logger.LogError("Failed to create parent: {Message}", message);

            return NewResult(responseResult);
        }
        /// <summary>
        /// Updates the authenticated parent's profile.
        /// </summary>
        /// <param name="dto">Updated parent profile data.</param>
        /// <returns>A status result indicating success or failure.</returns>
        [HttpPut("profile")]
        //[Authorize(Roles = "Parent")]
        public async Task<IActionResult> UpdateParentProfile([FromBody] ParentUpdateDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                var response = new Response<string>("المستخدم غير مصرح له أو معرف المستخدم غير صحيح.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
                _logger.LogError("Update profile failed: Invalid UserId from token.");
                return NewResult(response);
            }

            if (!ModelState.IsValid)
            {
                var response = new Response<string>("البيانات المدخلة غير صحيحة", false)
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorsBag = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    )
                };
                return NewResult(response);
            }

            var (succeeded, message) = await _parentService.UpdateParentProfileAsync(userId, dto);

            var statusCode = succeeded ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest;
            var responseResult = new Response<string>(message, succeeded)
            {
                StatusCode = statusCode
            };

            if (!succeeded)
                _logger.LogError("Failed to update parent profile for UserId {UserId}: {Message}", userId, message);

            return NewResult(responseResult);
        }

        /// <summary>
        /// Retrieves the profile of the authenticated parent.
        /// </summary>
        /// <returns>The parent's profile data with students.</returns>
        [HttpGet("profile")]
        //[Authorize(Roles = "Parent")]
        public async Task<IActionResult> GetParentProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                var response = new Response<string>("المستخدم غير مصرح له أو معرف المستخدم غير صحيح.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
                _logger.LogError("Get profile failed: Invalid UserId from token.");
                return NewResult(response);
            }

            var parentDto = await _parentService.GetParentProfileAsync(userId);

            if (parentDto == null)
            {
                var response = new Response<string>("لم يتم العثور على ملف ولي الأمر.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return NewResult(response);
            }

            var result = new Response<ParentWithStudentsDto>(parentDto, "تم جلب بيانات ولي الأمر بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(result);
        }

        /// <summary>
        /// Retrieves a list of all parents in the system.
        /// </summary>
        /// <returns>A list of all parent entities.</returns>
        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentService.GetAllParentsAsync();
            var result = new Response<IEnumerable<Parent>>(parents, "تم جلب جميع أولياء الأمور بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
            return NewResult(result);
        }
    }
}
