using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Extensions;

namespace YemenSchoolsV1.API.Controllers
{
    public class SchoolReviewsController : AppControllerBase
    {

        private readonly ISchoolReviewService _service;

        public SchoolReviewsController(ISchoolReviewService service)
        {
            _service = service;
        }
        // GET: api/SchoolReviews/average/{schoolId}
        [HttpGet("average/{schoolId}")]
        public async Task<IActionResult> GetAverageRating(Guid schoolId)
        {
            var average = await _service.GetAverageRatingAsync(schoolId);
            return Ok(new { SchoolId = schoolId, AverageRating = average });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReview([FromBody] AddReviewDto dto)
        {
            var userId = User.GetUserId();
            var review = await _service.AddOrUpdateReviewAsync(dto.SchoolId, userId!, dto.Rating, dto.Comment);
            return Ok(review);
        }


        // GET: api/SchoolReviews/school/{schoolId}
        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetReviewsForSchool(Guid schoolId)
        {
            var reviews = await _service.GetReviewsForSchoolAsync(schoolId);
            return Ok(reviews);
        }



        // PUT: api/SchoolReviews/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewDto dto)
        {
            var userId = User.GetUserId();
            var review = await _service.UpdateReviewAsync(id, userId, dto.Rating, dto.Comment);
            return Ok(review);
        }

        // DELETE: api/SchoolReviews/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var userId = User.GetUserId();
            await _service.DeleteReviewAsync(id, userId);
            return NoContent();
        }
    }

    public class AddReviewDto
    {
        public Guid SchoolId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateReviewDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}

