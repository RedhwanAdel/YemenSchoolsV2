using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.Parents.Commands.CreateParent;
using YemenSchoolsV1.Application.Features.Parents.Commands.UpdateParentProfile;
using YemenSchoolsV1.Application.Features.Parents.Queries.CheckParentByNationalId;
using YemenSchoolsV1.Application.Features.Parents.Queries.GetAllParents;
using YemenSchoolsV1.Application.Features.Parents.Queries.GetParentProfile;
using YemenSchoolsV1.Application.Features.Parents.Queries.GetParentWithStudents;
using YemenSchoolsV1.Application.Features.Parents.Queries.GetStudentsWithSchoolInfo;
using YemenSchoolsV1.Application.Features.Parents;
using YemenSchoolsV1.Application.Features.Parents.Queries.GetTeachersForParent;
using YemenSchoolsV1.Application.Features.Parents.Queries.ParentExists;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class ParentsController : AppControllerBase
    {
        /// <summary>
        /// Retrieves all teachers who teach the children of the authenticated parent.
        /// </summary>
        /// <returns>List of teachers with related info.</returns>
        [HttpGet("teachers")]
        [ProducesResponseType(typeof(Response<List<TeacherInfoForParentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersForParent()
        {
            var parentId = User.GetEntityId();
            var response = await Mediator.Send(new GetTeachersForParentQuery(parentId));
            return NewResult(response);
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
            var response = await Mediator.Send(new GetStudentsWithSchoolInfoQuery(parentId));
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
            var response = await Mediator.Send(new GetParentWithStudentsQuery(parentId));
            return NewResult(response);
        }

        [HttpGet("check-national-id/{nationalId}")]
        [ProducesResponseType(typeof(Response<ParentCheckDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckParentByNationalId(string nationalId)
        {
            var response = await Mediator.Send(new CheckParentByNationalIdQuery(nationalId));
            return NewResult(response);
        }

        /// <summary>
        /// Checks if a parent exists by National ID.
        /// </summary>
        /// <param name="nationalId">The parent's National ID.</param>
        /// <returns>True if exists, false otherwise.</returns>
        [HttpGet("exist")]
        public async Task<IActionResult> IsParentExist([FromQuery] string nationalId)
        {
            var response = await Mediator.Send(new ParentExistsQuery(nationalId));
            return NewResult(response);
        }

        /// <summary>
        /// Creates a new parent and an associated user account.
        /// </summary>
        /// <param name="dto">Parent creation data.</param>
        /// <returns>A status result indicating success or failure.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] ParentCreateDto dto)
        {
            var response = await Mediator.Send(new CreateParentCommand(dto));
            return NewResult(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateParentProfile([FromBody] ParentUpdateDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return NewResult(new Response<string>("المستخدم غير مصرح له أو معرف المستخدم غير صحيح.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });
            }

            var response = await Mediator.Send(new UpdateParentProfileCommand(userId, dto));
            return NewResult(response);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetParentProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return NewResult(new Response<string>("المستخدم غير مصرح له أو معرف المستخدم غير صحيح.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });
            }

            var response = await Mediator.Send(new GetParentProfileQuery(userId));
            return NewResult(response);
        }

        /// <summary>
        /// Retrieves a list of all parents in the system.
        /// </summary>
        /// <returns>A list of all parent entities.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllParents()
        {
            var response = await Mediator.Send(new GetAllParentsQuery());
            return NewResult(response);
        }
    }
}
