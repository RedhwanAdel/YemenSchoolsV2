using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;

using YemenSchoolsV1.Application.Features.SchoolGrades.Commands.Sync;
using YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetStageGrades;
using YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetGrades;
using  YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.API.Controllers
{
    public class SchoolGradeController : AppControllerBase
    {
        [HttpPost("sync-stage-grades")]
        public async Task<IActionResult> SyncStageGrades(CreateSchoolGradeDto request)
        {
            if (request.SchoolId == Guid.Empty)
                return NewResult(new Response<string>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });

            if (request.StageGradeIds == null || request.StageGradeIds.Count == 0)
                return NewResult(new Response<string>("يجب اختيار صفوف ومراحل على الأقل.", false) { StatusCode = HttpStatusCode.BadRequest });

            var response = await Mediator.Send(new SyncSchoolStageGradesCommand(request));
            return NewResult(response);
        }

        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetStageGradesForSchool(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
                return NewResult(new Response<List<StageGradeDto>>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });
            
            var response = await Mediator.Send(new GetStageGradesForSchoolQuery(schoolId));
            return NewResult(response);
        }

        [HttpGet("grade/{schoolId}")]
        public async Task<IActionResult> GetGradesForSchool(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
                return NewResult(new Response<List<SchoolGradeDto>>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });
            
            var response = await Mediator.Send(new GetGradesForSchoolQuery(schoolId));
            return NewResult(response);
        }
    }
}
