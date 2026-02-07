using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Commands.AddOrUpdateReview
{
    public class AddOrUpdateReviewCommandHandler : ResponseHandler, IRequestHandler<AddOrUpdateReviewCommand, Response<SchoolReview>>
    {
        private readonly ISchoolReviewRepository _repository;

        public AddOrUpdateReviewCommandHandler(
            ISchoolReviewRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
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
                return Success(existing, "Review updated successfully");
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
            return Created(review, "Review added successfully");
        }
    }
}
