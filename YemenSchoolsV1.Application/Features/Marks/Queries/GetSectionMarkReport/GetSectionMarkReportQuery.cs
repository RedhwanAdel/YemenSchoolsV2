using MediatR;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport
{
    public class GetSectionMarkReportQuery : IRequest<SectionMarkReportDto>
    {
        public Guid SectionSubjectId { get; set; }
    }
}
