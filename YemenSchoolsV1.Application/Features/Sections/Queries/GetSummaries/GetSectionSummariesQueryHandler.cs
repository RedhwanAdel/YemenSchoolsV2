using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetSummaries
{
    public class GetSectionSummariesQueryHandler : ResponseHandler, IRequestHandler<GetSectionSummariesQuery, Response<List<SectionSummaryDto>>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionSummariesQueryHandler(
            ISectionRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<SectionSummaryDto>>> Handle(GetSectionSummariesQuery request, CancellationToken cancellationToken)
        {
             if (request.AcademicYearId == Guid.Empty)
                return BadRequest<List<SectionSummaryDto>>("Invalid academic year ID.");

            var summaries = await _repository.GetSectionSummariesByAcademicYearAsync(request.AcademicYearId);
            return Success(summaries);
        }
    }
}
