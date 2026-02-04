using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetTeachersForParent
{
    public class GetTeachersForParentQuery : IRequest<Response<List<TeacherInfoForParentDto>>>
    {
        public Guid ParentId { get; set; }

        public GetTeachersForParentQuery(Guid parentId)
        {
            ParentId = parentId;
        }
    }
}
