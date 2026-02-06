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
            var response = await Mediator.Send(new SyncSchoolStageGradesCommand(request));
            return NewResult(response);
        }

        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetStageGradesForSchool(Guid schoolId)
        {
            var response = await Mediator.Send(new GetStageGradesForSchoolQuery(schoolId));
            return NewResult(response);
        }

        [HttpGet("grade/{schoolId}")]
        public async Task<IActionResult> GetGradesForSchool(Guid schoolId)
        {
            var response = await Mediator.Send(new GetGradesForSchoolQuery(schoolId));
            return NewResult(response);
        }
    }
}
