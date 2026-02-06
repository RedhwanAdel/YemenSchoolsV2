using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Stages.Commands.Create;
using YemenSchoolsV1.Application.Features.Stages.Commands.Delete;
using YemenSchoolsV1.Application.Features.Stages.Commands.Update;
using YemenSchoolsV1.Application.Features.Stages.Queries.GetAll;

namespace YemenSchoolsV1.API.Controllers
{
    public class StagesController : AppControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetStagesListQueary());
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditStageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteStageCommand(id));
            return NewResult(response);
        }
    }
}
