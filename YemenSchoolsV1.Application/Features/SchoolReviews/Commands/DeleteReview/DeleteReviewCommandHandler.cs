using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Response<string>>
    {
        private readonly ISchoolReviewRepository _repository;

        public DeleteReviewCommandHandler(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                return new Response<string>("Review not found.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (review.UserId != request.UserId)
            {
                return new Response<string>("You cannot delete someone else’s review.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }

            await _repository.DeleteAsync(review);
            return new Response<string>("Review deleted successfully.", true)
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
    }
}
