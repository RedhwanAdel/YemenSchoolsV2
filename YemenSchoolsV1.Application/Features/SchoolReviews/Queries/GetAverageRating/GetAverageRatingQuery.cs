using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetAverageRating
{
    public class GetAverageRatingQuery : IRequest<Response<object>>
    {
        public Guid SchoolId { get; set; }

        public GetAverageRatingQuery(Guid schoolId)
        {
            SchoolId = schoolId;
        }
    }
}
