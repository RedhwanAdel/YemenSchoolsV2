using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetAverageRating
{
    public class GetAverageRatingQueryHandler : IRequestHandler<GetAverageRatingQuery, Response<object>>
    {
        private readonly ISchoolReviewRepository _repository;

        public GetAverageRatingQueryHandler(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<object>> Handle(GetAverageRatingQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetBySchoolIdAsync(request.SchoolId);
            double average = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

            return new Response<object>(new { SchoolId = request.SchoolId, AverageRating = average }, "Success")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
