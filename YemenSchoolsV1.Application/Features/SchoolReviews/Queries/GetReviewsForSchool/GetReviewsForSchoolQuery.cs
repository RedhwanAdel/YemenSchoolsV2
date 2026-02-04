using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolReviews.Queries.GetReviewsForSchool
{
    public class GetReviewsForSchoolQuery : IRequest<Response<IEnumerable<SchoolReviewDto>>>
    {
        public Guid SchoolId { get; set; }

        public GetReviewsForSchoolQuery(Guid schoolId)
        {
            SchoolId = schoolId;
        }
    }
}
