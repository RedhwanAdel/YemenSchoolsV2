using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Dashboard.Queries
{
    public class GetDashboardQuery : IRequest<Response<DashboardDto>>
    {
    }
}
