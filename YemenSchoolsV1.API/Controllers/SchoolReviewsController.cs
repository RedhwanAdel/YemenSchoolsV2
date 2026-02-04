using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Features.SchoolReviews.Commands.AddOrUpdateReview;
using YemenSchoolsV1.Application.Features.SchoolReviews.Commands.DeleteReview;
using YemenSchoolsV1.Application.Features.SchoolReviews.Commands.UpdateReview;
using YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetAverageRating;
using YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetReviewsForSchool;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class SchoolReviewsController : AppControllerBase
    {
        // GET: api/SchoolReviews/average/{schoolId}
        [HttpGet("average/{schoolId}")]
        public async Task<IActionResult> GetAverageRating(Guid schoolId)
        {
            var response = await Mediator.Send(new GetAverageRatingQuery(schoolId));
            return NewResult(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReview([FromBody] AddReviewDto dto)
        {
            var userId = User.GetUserId();
            var response = await Mediator.Send(new AddOrUpdateReviewCommand(dto.SchoolId, userId!, dto.Rating, dto.Comment));
            return NewResult(response);
        }


        // GET: api/SchoolReviews/school/{schoolId}
        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetReviewsForSchool(Guid schoolId)
        {
            var response = await Mediator.Send(new GetReviewsForSchoolQuery(schoolId));
            return NewResult(response);
        }



        // PUT: api/SchoolReviews/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewDto dto)
        {
            var userId = User.GetUserId();
            var response = await Mediator.Send(new UpdateReviewCommand(id, userId, dto.Rating, dto.Comment));
            return NewResult(response);
        }

        // DELETE: api/SchoolReviews/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var userId = User.GetUserId();
            var response = await Mediator.Send(new DeleteReviewCommand(id, userId));
            return NewResult(response);
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

