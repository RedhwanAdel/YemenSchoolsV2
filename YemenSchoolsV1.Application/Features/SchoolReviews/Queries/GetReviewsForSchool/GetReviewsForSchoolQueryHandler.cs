using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetReviewsForSchool
{
    public class GetReviewsForSchoolQueryHandler : IRequestHandler<GetReviewsForSchoolQuery, Response<IEnumerable<SchoolReviewDto>>>
    {
        private readonly ISchoolReviewRepository _repository;

        public GetReviewsForSchoolQueryHandler(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<SchoolReviewDto>>> Handle(GetReviewsForSchoolQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetBySchoolIdAsync(request.SchoolId);
            return new Response<IEnumerable<SchoolReviewDto>>(reviews, "Success")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
