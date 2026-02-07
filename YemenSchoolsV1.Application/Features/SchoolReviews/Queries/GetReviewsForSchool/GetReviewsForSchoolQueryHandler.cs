using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetReviewsForSchool
{
    public class GetReviewsForSchoolQueryHandler : ResponseHandler, IRequestHandler<GetReviewsForSchoolQuery, Response<IEnumerable<SchoolReviewDto>>>
    {
        private readonly ISchoolReviewRepository _repository;

        public GetReviewsForSchoolQueryHandler(
            ISchoolReviewRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<SchoolReviewDto>>> Handle(GetReviewsForSchoolQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetBySchoolIdAsync(request.SchoolId);
            return Success(reviews);
        }
    }
}
