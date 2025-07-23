using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.API.Dto;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.Grades.Commands.Create;
using YemenSchoolsV1.Application.Features.Grades.Commands.Delete;
using YemenSchoolsV1.Application.Features.Grades.Commands.Update;
using YemenSchoolsV1.Application.Features.Grades.Queries;

namespace YemenSchoolsV1.API.Controllers
{

    public class GradesController(IStageGradeRepositry stageGradeRepositry, IMapper mapper) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetGradesListQueary());
            return Ok(response);
        }


        [HttpGet("stageGrades")]
        public async Task<IActionResult> GetAllStageGrades()
        {
            var response = await stageGradeRepositry.GetAllStageGradesAsync();
            var stageGrad = mapper.Map<List<StageGradeDto>>(response);
            return Ok(stageGrad);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditGradeCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteGradeCommand(id));
            return NewResult(response);
        }
    }
}
