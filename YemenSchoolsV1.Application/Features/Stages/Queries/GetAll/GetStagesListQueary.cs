using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
    public class GetStagesListQueary : IRequest<Response<List<GetStagesListResponse>>>
    {

    }
}
