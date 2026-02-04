using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.SetCurrentYear;
using YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetCurrentYearId;

namespace YemenSchoolsV1.API.Controllers
{

    public class AcademicYearsController : AppControllerBase
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
            var response = await Mediator.Send(new SetCurrentYearCommand(schoolId, academicYearId));
            return NewResult(response);
        }
        [HttpGet("{schoolId:guid}/current-year-id")]
        public async Task<IActionResult> GetCurrentYearId([FromRoute] Guid schoolId)
        {
            var response = await Mediator.Send(new GetCurrentYearIdQuery(schoolId));
            return NewResult(response);
        }
    }
}
