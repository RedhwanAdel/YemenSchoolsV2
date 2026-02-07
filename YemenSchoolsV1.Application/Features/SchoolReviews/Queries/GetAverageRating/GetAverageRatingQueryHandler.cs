using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetAverageRating
{
    public class GetAverageRatingQueryHandler : ResponseHandler, IRequestHandler<GetAverageRatingQuery, Response<object>>
    {
        private readonly ISchoolReviewRepository _repository;

        public GetAverageRatingQueryHandler(
            ISchoolReviewRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<object>> Handle(GetAverageRatingQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetBySchoolIdAsync(request.SchoolId);
            double average = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

            return Success((object)new { SchoolId = request.SchoolId, AverageRating = average });
        }
    }
}
