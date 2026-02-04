using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolReport
{
    public class GetSchoolReportQuery : IRequest<Response<SchoolReportDto>>
    {
        public Guid Id { get; set; }
        public GetSchoolReportQuery(Guid id) => Id = id;
    }
}
