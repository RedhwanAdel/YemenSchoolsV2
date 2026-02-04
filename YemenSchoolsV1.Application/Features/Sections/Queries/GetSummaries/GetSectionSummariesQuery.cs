using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetSummaries
{
    public class GetSectionSummariesQuery : IRequest<Response<List<SectionSummaryDto>>>
    {
        public Guid AcademicYearId { get; set; }
        public GetSectionSummariesQuery(Guid academicYearId) => AcademicYearId = academicYearId;
    }
}
