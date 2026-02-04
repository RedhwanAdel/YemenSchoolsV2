using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Dashboard.Queries
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Response<DashboardDto>>
    {
        private readonly IDashboardRepository _dashboardRepo;

        public GetDashboardQueryHandler(IDashboardRepository dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        public async Task<Response<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepo.GetDashboardAsync();
            return new Response<DashboardDto>(result);
        }
    }
}
