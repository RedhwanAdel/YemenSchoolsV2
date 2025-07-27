using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.API.Dto;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.API.Controllers
{
    public class SchoolGradeController(ISchoolGradeRepository schoolGradeRepository) : AppControllerBase
    {
        [HttpPost("sync-stage-grades")]
        public async Task<IActionResult> SyncStageGrades(CreateSchoolGradeDto request)
        {
            if (request.SchoolId == Guid.Empty)
                return NewResult(new Response<string>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });

            if (request.StageGradeIds == null || request.StageGradeIds.Count == 0)
                return NewResult(new Response<string>("يجب اختيار صفوف ومراحل على الأقل.", false) { StatusCode = HttpStatusCode.BadRequest });

            await schoolGradeRepository.SyncSchoolStageGradesAsync(request.SchoolId, request.StageGradeIds);

            return NewResult(new Response<string>("تم حفظ إعدادات الصفوف والمراحل بنجاح.") { StatusCode = HttpStatusCode.OK, Succeeded = true });

        }
        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetStageGradesForSchool(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
                return NewResult(new Response<List<StageGradeDto>>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });
            var result = await schoolGradeRepository.GetStageGradesAsync(schoolId);
            return NewResult(new Response<List<StageGradeDto>>(result) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        [HttpGet("grade/{schoolId}")]
        public async Task<IActionResult> GetGradesForSchool(Guid schoolId)
        {
            if (schoolId == Guid.Empty)
                return NewResult(new Response<List<SchoolGradeDto>>("معرف المدرسة غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });
            var result = await schoolGradeRepository.GetSchoolGradesBySchoolIdAsync(schoolId);
            return NewResult(new Response<List<SchoolGradeDto>>(result) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }



    }
}
