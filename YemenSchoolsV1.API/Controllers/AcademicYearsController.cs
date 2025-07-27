using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;

namespace YemenSchoolsV1.API.Controllers
{

    public class AcademicYearsController(IAcademicYearRepository academicYearRepository) : AppControllerBase
    {
        [HttpGet("{schoolId}")]

        public async Task<IActionResult> GetAll([FromRoute] Guid schoolId)
        {
            var response = await Mediator.Send(new GetYearListQueary(schoolId));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateYearCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditYearCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteYearCommand(id));
            return NewResult(response);
        }

        [HttpPut("{schoolId:guid}/set-current/{academicYearId:guid}")]
        public async Task<IActionResult> SetCurrentYear([FromRoute] Guid schoolId, [FromRoute] Guid academicYearId)
        {
            var result = await academicYearRepository.SetCurrentYearAsync(schoolId, academicYearId);
            if (result == null)
                return NewResult(new Response<string>("Academic year not found or school has no years.", false) { StatusCode = System.Net.HttpStatusCode.NotFound });

            return NewResult(new Response<Guid>(result.Value, "Current academic year set successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true });
        }
        [HttpGet("{schoolId:guid}/current-year-id")]
        public async Task<IActionResult> GetCurrentYearId([FromRoute] Guid schoolId)
        {
            var result = await academicYearRepository.GetCurrentYearIdAsync(schoolId);
            if (result == null)
                return NewResult(new Response<string>("No current academic year found for this school.", false) { StatusCode = System.Net.HttpStatusCode.NotFound });

            return NewResult(new Response<Guid>(result.Value, "Current academic year ID retrieved successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true });
        }
    }
}
