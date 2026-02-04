using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Response<SchoolReview>>
    {
        private readonly ISchoolReviewRepository _repository;

        public UpdateReviewCommandHandler(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolReview>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                return new Response<SchoolReview>("Review not found.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (review.UserId != request.UserId)
            {
                return new Response<SchoolReview>("You cannot edit someone else’s review.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(review);
            return new Response<SchoolReview>(review, "Review updated successfully")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
