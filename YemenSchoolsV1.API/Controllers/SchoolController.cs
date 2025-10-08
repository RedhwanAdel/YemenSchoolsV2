using FinalProject.Application.Bases;
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
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.API.Controllers
{

    public class SchoolController(ISchoolRepositry schoolRepositry, YemenShoolsDbContext _context) : AppControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Paginated([FromQuery] GetSchoolPagenatedListQueary query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
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
            var school = await schoolRepositry.GetSchoolByIdForUpdateAsync(id);
            if (school == null)
            {
                return NotFound(new { Message = "School not found" });
            }
            return Ok(school);
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
            if (request.SchoolGradeId == Guid.Empty)
                return NewResult(new Response<string>("معرف الصف غير صالح.", false) { StatusCode = HttpStatusCode.BadRequest });

            await schoolRepositry.AssignSubjectsToSchoolGradeAsync(request.SchoolGradeId, request.SubjectIds);

            return NewResult(new Response<string>("تم حفظ إعدادات المواد بنجاح.") { StatusCode = HttpStatusCode.OK, Succeeded = true });

        }


        [HttpGet("{schoolGradeId}/subjects")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsForSchoolGrade(Guid schoolGradeId)
        {

            var subjects = await schoolRepositry.GetSubjectsForSchoolGradeAsync(schoolGradeId);
            return NewResult(new Response<List<SubjectDto>>(subjects) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpGet("{id:guid}/report")]
        public async Task<IActionResult> GetSchoolReport([FromRoute] Guid id)
        {
            var report = await schoolRepositry.GetSchoolReportAsync(id);
            if (report == null)
                return NewResult(new Response<string>("School not found.", false) { StatusCode = HttpStatusCode.NotFound });

            return NewResult(new Response<SchoolReportDto>(report) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        [HttpPost("{schoolId}/upload")]
        public async Task<IActionResult> UploadSchoolPhoto(IFormFile file, Guid schoolId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            string folder = Path.Combine("wwwroot", "uploads", "schools");
            Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string fileUrl = $"https://localhost:5001/uploads/schools/{fileName}";

            var schoolPhoto = new SchoolPhoto
            {
                SchoolId = schoolId,
                PhotoUrl = fileUrl
            };

            _context.SchoolPhotos.Add(schoolPhoto);
            await _context.SaveChangesAsync();

            return Ok(schoolPhoto);
        }
        [HttpGet("{schoolId}/photos")]
        public async Task<IActionResult> GetSchoolPhotos(Guid schoolId)
        {
            var photos = await _context.SchoolPhotos
                .Where(p => p.SchoolId == schoolId)
                .ToListAsync();

            return Ok(photos);
        }



    }
}
