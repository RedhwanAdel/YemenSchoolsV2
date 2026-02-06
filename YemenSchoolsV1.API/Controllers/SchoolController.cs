using YemenSchoolsV1.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons;
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.UpdateSchool;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Application.Features.Schools.Commands.AssignSubjects;
using YemenSchoolsV1.Application.Features.Schools.Commands.UploadPhoto;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolByIdForUpdate;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolPhotos;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolReport;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSubjectsForSchoolGrade;

namespace YemenSchoolsV1.API.Controllers
{

    public class SchoolController : AppControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Paginated([FromQuery] GetSchoolPagenatedListQueary query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSchoolDetailsQuery(id));
            return NewResult(response);
        }

        [HttpGet("GetSchoolByIdForUpdate/{id}")]
        public async Task<IActionResult> GetSchoolByIdForUpdate([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSchoolByIdForUpdateQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditSchoolForAdminCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }

        [HttpPost("AddPhonsToSchool")]

        public async Task<IActionResult> AddPhonsToSchool([FromBody] CreateSchoolPhonsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteSchoolCommand(id));
            return NewResult(response);
        }




        [HttpPost("assign-grade-subjects")]
        public async Task<IActionResult> AssignSubjectsToSchoolGrade([FromBody] AssignSubjectsToSchoolGradeDto request)
        {
            var response = await Mediator.Send(new AssignSubjectsToSchoolGradeCommand(request));
            return NewResult(response);

        }


        [HttpGet("{schoolGradeId}/subjects")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsForSchoolGrade(Guid schoolGradeId)
        {
            var response = await Mediator.Send(new GetSubjectsForSchoolGradeQuery(schoolGradeId));
            return NewResult(response);
        }


        [HttpGet("{id:guid}/report")]
        public async Task<IActionResult> GetSchoolReport([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSchoolReportQuery(id));
            return NewResult(response);
        }

        [HttpPost("{schoolId}/upload")]
        public async Task<IActionResult> UploadSchoolPhoto(IFormFile file, Guid schoolId)
        {
            var response = await Mediator.Send(new UploadSchoolPhotoCommand(file, schoolId));
            return NewResult(response);
        }
        [HttpGet("{schoolId}/photos")]
        public async Task<IActionResult> GetSchoolPhotos(Guid schoolId)
        {
            var response = await Mediator.Send(new GetSchoolPhotosQuery(schoolId));
            return NewResult(response);
        }



    }
}
