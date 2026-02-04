using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.UpdateReview
{
    public class UpdateReviewCommand : IRequest<Response<SchoolReview>>
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public UpdateReviewCommand(Guid reviewId, Guid userId, int rating, string? comment)
        {
            ReviewId = reviewId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
        }
    }
}
