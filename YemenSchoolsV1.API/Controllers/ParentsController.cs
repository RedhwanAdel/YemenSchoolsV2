using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Parents;

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
        /// [POST] /api/parent
        /// Creates a new parent and an associated user account.
        /// هذا الإجراء مُصمَّم للاستخدام من قبل مسؤول النظام (Admin) أو من خلال عملية تسجيل.
        /// </summary>
        /// <param name="dto">Parent creation data.</param>
        /// <returns>A status result indicating success or failure.</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")] // يجب أن يكون المستخدم مسؤولاً لتنفيذ هذا الإجراء
        public async Task<IActionResult> CreateParent([FromBody] ParentCreateDto dto)
        {
            // استخدام كلمة مرور افتراضية، يفضل إرسالها من الـ DTO أو توليدها بشكل عشوائي.
            string defaultPassword = "DefaultPassword123!";

            var (succeeded, message) = await _parentService.CreateParentWithUserAsync(dto, defaultPassword);

            if (!succeeded)
            {
                _logger.LogError("Failed to create parent: {message}", message);
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        /// <summary>
        /// [PUT] /api/parent/profile
        /// Updates the authenticated parent's profile.
        /// </summary>
        /// <param name="dto">Updated parent profile data.</param>
        /// <returns>A status result indicating success or failure.</returns>
        [HttpPut("profile")]
        //[Authorize(Roles = "Parent")] // فقط أولياء الأمور يمكنهم تعديل ملفاتهم الشخصية
        public async Task<IActionResult> UpdateParentProfile([FromBody] ParentUpdateDto dto)
        {
            // استخراج معرف المستخدم (UserId) من الرمز المميز (token) الخاص بالمستخدم الحالي.
            // هذا يضمن أن المستخدم يمكنه فقط تحديث ملفه الشخصي.
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                _logger.LogError("Update profile failed: Invalid UserId from token.");
                return Unauthorized(new { message = "المستخدم غير مصرح له أو معرف المستخدم غير صحيح." });
            }

            var (succeeded, message) = await _parentService.UpdateParentProfileAsync(userId, dto);

            if (!succeeded)
            {
                _logger.LogError("Failed to update parent profile for UserId {userId}: {message}", userId, message);
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        /// <summary>
        /// [GET] /api/parent/profile
        /// Retrieves the profile of the authenticated parent.
        /// </summary>
        /// <returns>The parent's profile data with students.</returns>
        [HttpGet("profile")]
        //[Authorize(Roles = "Parent")] // فقط أولياء الأمور يمكنهم عرض ملفاتهم الشخصية
        public async Task<IActionResult> GetParentProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                _logger.LogError("Get profile failed: Invalid UserId from token.");
                return Unauthorized(new { message = "المستخدم غير مصرح له أو معرف المستخدم غير صحيح." });
            }

            var parentDto = await _parentService.GetParentProfileAsync(userId);

            if (parentDto == null)
            {
                return NotFound(new { message = "لم يتم العثور على ملف ولي الأمر." });
            }

            return Ok(parentDto);
        }

        /// <summary>
        /// [GET] /api/parent/all
        /// Retrieves a list of all parents in the system.
        /// </summary>
        /// <returns>A list of all parent entities.</returns>
        [HttpGet("all")]
        //[Authorize(Roles = "Admin")] // متاح للمسؤولين فقط
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _parentService.GetAllParentsAsync();
            return Ok(parents);
        }
    }
}
