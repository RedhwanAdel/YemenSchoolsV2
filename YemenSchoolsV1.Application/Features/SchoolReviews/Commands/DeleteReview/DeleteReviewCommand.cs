using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }

        public DeleteReviewCommand(Guid reviewId, Guid userId)
        {
            ReviewId = reviewId;
            UserId = userId;
        }
    }
}
