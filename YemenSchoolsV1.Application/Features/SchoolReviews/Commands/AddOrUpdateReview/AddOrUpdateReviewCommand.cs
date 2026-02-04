using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.AddOrUpdateReview
{
    public class AddOrUpdateReviewCommand : IRequest<Response<SchoolReview>>
    {
        public Guid SchoolId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public AddOrUpdateReviewCommand(Guid schoolId, Guid userId, int rating, string? comment)
        {
            SchoolId = schoolId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
        }
    }
}
