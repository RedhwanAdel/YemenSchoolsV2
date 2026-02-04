using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.AddOrUpdateReview
{
    public class AddOrUpdateReviewCommandHandler : IRequestHandler<AddOrUpdateReviewCommand, Response<SchoolReview>>
    {
        private readonly ISchoolReviewRepository _repository;

        public AddOrUpdateReviewCommandHandler(ISchoolReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolReview>> Handle(AddOrUpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetBySchoolAndUserAsync(request.SchoolId, request.UserId);

            if (existing != null)
            {
                existing.Rating = request.Rating;
                existing.Comment = request.Comment;
                existing.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(existing);
                return new Response<SchoolReview>(existing, "Review updated successfully")
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Succeeded = true
                };
            }

            var review = new SchoolReview
            {
                Id = Guid.NewGuid(),
                SchoolId = request.SchoolId,
                UserId = request.UserId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(review);
            return new Response<SchoolReview>(review, "Review added successfully")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
