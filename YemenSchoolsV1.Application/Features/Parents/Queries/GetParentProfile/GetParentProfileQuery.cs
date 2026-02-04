using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetParentProfile
{
    public class GetParentProfileQuery : IRequest<Response<ParentWithStudentsDto>>
    {
        public Guid UserId { get; set; }

        public GetParentProfileQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
