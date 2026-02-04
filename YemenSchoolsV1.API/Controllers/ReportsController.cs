using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Enums;
using YemenSchoolsV1.Application.Features.Reports.Queries.GetStudentReport;
using YemenSchoolsV1.Application.Features.Reports.Queries.GetSchoolReport;

namespace YemenSchoolsV1.API.Controllers
{
    public class ReportsController : AppControllerBase
    {
        [HttpPost("student/{id}")]
        public async Task<IActionResult> GetStudentReport(Guid id)
        {
             var response = await Mediator.Send(new GetStudentReportQuery(id));
             if (!response.Succeeded) return NewResult(response);
             
             var file = response.Data;
             return File(file.FileContents, file.ContentType, file.FileName);
        }

        [HttpPost("school/{schoolId:guid}")]
        public async Task<IActionResult> GetSchoolReport(Guid schoolId)
        {
            var response = await Mediator.Send(new GetSchoolReportQuery(schoolId));
            if (!response.Succeeded) return NewResult(response);

            var file = response.Data;
            return File(file.FileContents, file.ContentType, file.FileName);
        }
    }
}
