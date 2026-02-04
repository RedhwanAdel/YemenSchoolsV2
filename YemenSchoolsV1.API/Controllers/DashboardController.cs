using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Dashboard.Queries;

namespace YemenSchoolsV1.API.Controllers
{
    public class DashboardController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var response = await Mediator.Send(new GetDashboardQuery());
            return Ok(response.Data); 
        }
    }
}
