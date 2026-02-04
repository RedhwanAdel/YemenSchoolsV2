using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

using YemenSchoolsV1.Application.Features.Sections.Commands.Create;
using YemenSchoolsV1.Application.Features.Sections.Commands.Delete;
using YemenSchoolsV1.Application.Features.Sections.Commands.Update;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetByGradeAndYear;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetById;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetByTeacherId;
using YemenSchoolsV1.Application.Features.Sections.Queries.GetSummaries;

namespace YemenSchoolsV1.API.Controllers
{

    public class SectionsController : AppControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            var response = await Mediator.Send(new GetSectionByIdQuery(id));
            return NewResult(response);
        }

        [HttpGet("by-academic-year-and-grade")]
        public async Task<IActionResult> GetByAcademicYearAndSchoolGrade([FromQuery] Guid academicYearId, [FromQuery] Guid schoolGradeId)
        {
            var response = await Mediator.Send(new GetSectionsByGradeAndYearQuery(academicYearId, schoolGradeId));
            return NewResult(response);
        }

        [HttpGet("by-teacherId/{teacherId:guid}")]
        public async Task<IActionResult> GetByTeacherId(Guid teacherId)
        {
            var response = await Mediator.Send(new GetSectionsByTeacherIdQuery(teacherId));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectionDto dto)
        {
            var response = await Mediator.Send(new CreateSectionCommand(dto));
            return NewResult(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSectionDto dto)
        {
            var response = await Mediator.Send(new UpdateSectionCommand(id, dto));
            return NewResult(response);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteSectionCommand(id));
            return NewResult(response);
        }


        [HttpGet("by-academic-year")]
        public async Task<IActionResult> GetSectionSummariesByAcademicYear([FromQuery] Guid academicYearId)
        {
            var response = await Mediator.Send(new GetSectionSummariesQuery(academicYearId));
            return NewResult(response);
        }


    }
}
