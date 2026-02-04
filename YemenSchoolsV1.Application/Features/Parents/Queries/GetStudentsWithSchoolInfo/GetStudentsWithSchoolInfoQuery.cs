using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetStudentsWithSchoolInfo
{
    public class GetStudentsWithSchoolInfoQuery : IRequest<Response<List<StudentWithSchoolInfoDto>>>
    {
        public Guid ParentId { get; set; }

        public GetStudentsWithSchoolInfoQuery(Guid parentId)
        {
            ParentId = parentId;
        }
    }
}
