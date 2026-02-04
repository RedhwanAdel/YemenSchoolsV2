using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetParentWithStudents
{
    public class GetParentWithStudentsQuery : IRequest<Response<ParentWithStudentsDto>>
    {
        public Guid ParentId { get; set; }

        public GetParentWithStudentsQuery(Guid parentId)
        {
            ParentId = parentId;
        }
    }
}
