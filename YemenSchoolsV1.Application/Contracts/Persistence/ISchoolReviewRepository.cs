using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolReviewRepository
    {
        Task<double> GetAverageRatingAsync(Guid schoolId);

        Task<SchoolReview?> GetByIdAsync(Guid id);
        Task<IEnumerable<SchoolReviewDto>> GetBySchoolIdAsync(Guid schoolId);
        Task<SchoolReview?> GetBySchoolAndUserAsync(Guid schoolId, Guid userId);
        Task AddAsync(SchoolReview review);
        Task UpdateAsync(SchoolReview review);
        Task DeleteAsync(SchoolReview review);
    }

}
