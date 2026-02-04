using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolPhotos
{
    public class GetSchoolPhotosQuery : IRequest<Response<List<SchoolPhoto>>>
    {
        public Guid SchoolId { get; set; }
        public GetSchoolPhotosQuery(Guid schoolId) => SchoolId = schoolId;
    }
}
