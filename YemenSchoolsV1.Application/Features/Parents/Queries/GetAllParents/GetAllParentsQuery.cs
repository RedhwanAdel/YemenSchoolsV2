using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetAllParents
{
    public class GetAllParentsQuery : IRequest<Response<IEnumerable<Parent>>>
    {
    }
}
