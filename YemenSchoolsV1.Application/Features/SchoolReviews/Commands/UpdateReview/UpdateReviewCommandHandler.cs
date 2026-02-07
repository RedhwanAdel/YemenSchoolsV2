using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : ResponseHandler, IRequestHandler<UpdateReviewCommand, Response<SchoolReview>>
    {
        private readonly ISchoolReviewRepository _repository;

        public UpdateReviewCommandHandler(
            ISchoolReviewRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolReview>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                return NotFound<SchoolReview>("Review not found.");
            }

            if (review.UserId != request.UserId)
            {
                return Unauthorized<SchoolReview>("You cannot edit someone else's review.");
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(review);
            return Success(review, "Review updated successfully");
        }
    }
}
