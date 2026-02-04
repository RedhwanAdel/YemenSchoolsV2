using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolByIdForUpdate
{
    public class GetSchoolByIdForUpdateQuery : IRequest<Response<SchoolForUpdate>>
    {
        public Guid Id { get; set; }
        public GetSchoolByIdForUpdateQuery(Guid id) => Id = id;
    }
}
