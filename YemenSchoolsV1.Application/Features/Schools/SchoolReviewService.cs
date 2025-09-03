using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools
{
    public class SchoolReviewService : ISchoolReviewService
    {
        private readonly ISchoolReviewRepository _repository;

        public SchoolReviewService(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<double> GetAverageRatingAsync(Guid schoolId)
        {
            var reviews = await _repository.GetBySchoolIdAsync(schoolId);
            if (!reviews.Any())
                return 0.0;

            return reviews.Average(r => r.Rating);
        }

        public async Task<IEnumerable<SchoolReviewDto>> GetReviewsForSchoolAsync(Guid schoolId)
        {
            return await _repository.GetBySchoolIdAsync(schoolId);
        }
        public async Task<SchoolReview> AddOrUpdateReviewAsync(Guid schoolId, Guid userId, int rating, string? comment)
        {
            var existing = await _repository.GetBySchoolAndUserAsync(schoolId, userId);

            if (existing != null)
            {
                // تعديل التقييم الموجود
                existing.Rating = rating;
                existing.Comment = comment;
                existing.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(existing);
                return existing;
            }

            // إضافة تقييم جديد
            var review = new SchoolReview
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(review);
            return review;
        }

        public async Task<SchoolReview> AddReviewAsync(Guid schoolId, Guid userId, int rating, string? comment)
        {
            var existing = await _repository.GetBySchoolAndUserAsync(schoolId, userId);
            if (existing != null)
                throw new InvalidOperationException("لقد قمت بتقييم هذه المدرسة بالفعل!");

            var review = new SchoolReview
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(review);
            return review;
        }

        public async Task<SchoolReview> UpdateReviewAsync(Guid reviewId, Guid userId, int rating, string? comment)
        {
            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null)
                throw new KeyNotFoundException("Review not found.");
            if (review.UserId != userId)
                throw new UnauthorizedAccessException("You cannot edit someone else’s review.");

            review.Rating = rating;
            review.Comment = comment;
            review.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(review);
            return review;
        }

        public async Task DeleteReviewAsync(Guid reviewId, Guid userId)
        {
            var review = await _repository.GetByIdAsync(reviewId);
            if (review == null)
                throw new KeyNotFoundException("Review not found.");
            if (review.UserId != userId)
                throw new UnauthorizedAccessException("You cannot delete someone else’s review.");

            await _repository.DeleteAsync(review);
        }
    }

}
