using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetSummaries
{
    public class GetSectionSummariesQueryHandler : IRequestHandler<GetSectionSummariesQuery, Response<List<SectionSummaryDto>>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionSummariesQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<SectionSummaryDto>>> Handle(GetSectionSummariesQuery request, CancellationToken cancellationToken)
        {
             if (request.AcademicYearId == Guid.Empty)
                return new Response<List<SectionSummaryDto>>("Invalid academic year ID.", false) { StatusCode = System.Net.HttpStatusCode.BadRequest };

            var summaries = await _repository.GetSectionSummariesByAcademicYearAsync(request.AcademicYearId);
            return new Response<List<SectionSummaryDto>>(summaries);
        }
    }
}
