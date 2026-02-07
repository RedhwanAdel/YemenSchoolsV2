using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : ResponseHandler, IRequestHandler<DeleteReviewCommand, Response<string>>
    {
        private readonly ISchoolReviewRepository _repository;

        public DeleteReviewCommandHandler(
            ISchoolReviewRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                return NotFound<string>("Review not found.");
            }

            if (review.UserId != request.UserId)
            {
                return new Response<string>("You cannot delete someone else’s review.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }

            await _repository.DeleteAsync(review);
            return Success("Review deleted successfully.");
        }
    }
}
