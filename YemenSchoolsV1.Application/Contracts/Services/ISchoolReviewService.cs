using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ISchoolReviewService
    {
        Task<double> GetAverageRatingAsync(Guid schoolId);
        Task<SchoolReview> AddOrUpdateReviewAsync(Guid schoolId, Guid userId, int rating, string? comment);

        Task<IEnumerable<SchoolReviewDto>> GetReviewsForSchoolAsync(Guid schoolId);
        Task<SchoolReview> AddReviewAsync(Guid schoolId, Guid userId, int rating, string? comment);
        Task<SchoolReview> UpdateReviewAsync(Guid reviewId, Guid userId, int rating, string? comment);
        Task DeleteReviewAsync(Guid reviewId, Guid userId);
    }

}
