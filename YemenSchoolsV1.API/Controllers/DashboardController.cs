using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.API.Controllers
{
    public class DashboardController : AppControllerBase
    {
        private readonly IDashboardRepository _dashboardRepo;

        public DashboardController(IDashboardRepository dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _dashboardRepo.GetDashboardAsync();
            return Ok(result);
        }
    }
}
