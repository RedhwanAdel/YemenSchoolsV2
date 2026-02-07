using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Dashboard.Queries
{
    public class GetDashboardQueryHandler : ResponseHandler, IRequestHandler<GetDashboardQuery, Response<DashboardDto>>
    {
        private readonly IDashboardRepository _dashboardRepo;

        public GetDashboardQueryHandler(
            IDashboardRepository dashboardRepo,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _dashboardRepo = dashboardRepo;
        }

        public async Task<Response<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepo.GetDashboardAsync();
            return Success(result);
        }
    }
}
