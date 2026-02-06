using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Features.Sections;
using YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetAll;
using YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetById;
using YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetBySectionId;
using YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Create;
using YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Update;
using YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Delete;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class SectionSubjectsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllSectionSubjectsQuery());
            return NewResult(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await Mediator.Send(new GetSectionSubjectByIdQuery(id));
            return NewResult(response);
        }

        [HttpGet("by-section/{sectionId:guid}")]
        public async Task<IActionResult> GetBySectionId(Guid sectionId)
        {
            var response = await Mediator.Send(new GetSectionSubjectsBySectionIdQuery(sectionId));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectionSubjectDto dto)
        {
            var response = await Mediator.Send(new CreateSectionSubjectCommand(dto));
            return NewResult(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SectionSubjecUpdateDto dto)
        {
            var response = await Mediator.Send(new UpdateSectionSubjectCommand(id, dto));
            return NewResult(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteSectionSubjectCommand(id));
            return NewResult(response);
        }
    }
}
