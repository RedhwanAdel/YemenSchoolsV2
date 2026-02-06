using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport
{
    public class GetSectionMarkReportQuery : IRequest<Response<SectionMarkReportDto>>
    {
        public Guid SectionSubjectId { get; set; }
    }
}
