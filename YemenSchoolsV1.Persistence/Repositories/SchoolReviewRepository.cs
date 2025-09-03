using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{

    public class SchoolReviewRepository : ISchoolReviewRepository
    {
        private readonly YemenShoolsDbContext _context;

        public SchoolReviewRepository(YemenShoolsDbContext context)
        {
            _context = context;
        }

        public async Task<SchoolReview?> GetByIdAsync(Guid id)
        {
            return await _context.schoolReviews
                .Include(r => r.User)
                .Include(r => r.School)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<SchoolReviewDto>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.schoolReviews
                .Where(r => r.SchoolId == schoolId)
                .Include(r => r.User)
                .Select(d => new SchoolReviewDto
                {
                    Id = d.Id,
                    SchoolId = d.SchoolId,
                    UserId = d.UserId,
                    UserName = d.User.Name,
                    UserImage = d.User.ImageUrl,
                    Rating = d.Rating,
                    Comment = d.Comment,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<SchoolReview?> GetBySchoolAndUserAsync(Guid schoolId, Guid userId)
        {
            return await _context.schoolReviews
                .FirstOrDefaultAsync(r => r.SchoolId == schoolId && r.UserId == userId);
        }

        public async Task AddAsync(SchoolReview review)
        {
            await _context.schoolReviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SchoolReview review)
        {
            _context.schoolReviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SchoolReview review)
        {
            _context.schoolReviews.Remove(review);
            await _context.SaveChangesAsync();
        }
        public async Task<double> GetAverageRatingAsync(Guid schoolId)
        {
            return await _context.schoolReviews
                .Where(r => r.SchoolId == schoolId)
                .AverageAsync(r => (double?)r.Rating) ?? 0.0;
        }

    }

}
