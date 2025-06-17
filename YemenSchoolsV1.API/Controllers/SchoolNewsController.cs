using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews;
using YemenSchoolsV1.Application.Features.SchoolsNews.Commands.DeleteSchoolNews;
using YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails;
using YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList;

namespace YemenSchoolsV1.API.Controllers
{
  
    public class SchoolNewsController : AppControllerBase
    {
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAllNewsBySchoolId([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetSchoolNewsListQuerey(id));
            return NewResult(response);
        }

        [HttpGet("GetSchoolNewsById/{newsId:guid}")]
        public async Task<IActionResult> GetSchoolNewssById([FromRoute] Guid newsId)
        {
            var response = await Mediator.Send(new GetSchoolNewsDetailsQueary(newsId));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolNewsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteSchoolNewsCommand(id));
            return NewResult(response);
        }

    }
}
