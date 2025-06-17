using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Subjects.Commands.Create;
using YemenSchoolsV1.Application.Features.Subjects.Commands.Delete;
using YemenSchoolsV1.Application.Features.Subjects.Commands.Update;
using YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll;
using YemenSchoolsV1.Application.Features.Subjects.Queries.GetById;

namespace YemenSchoolsV1.API.Controllers
{
  
    public class SubjectsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetSubjectsListQuery());
            return NewResult(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSubjectDetailsQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditSubjectCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteSubjectCommand(id));
            return NewResult(response);
        }
    }
}

