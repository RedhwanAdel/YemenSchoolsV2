using MediatR;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetAll
{
    public class GetTermsListQueary : IRequest<PaginatedResult<GetTermsListResponse>>
    {
        public PaginationQuery Pagination { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? AcadmicYearId { get; set; }


        public GetTermsListQueary(PaginationQuery pagination, Guid schoolId, Guid? acadmicYearId)
        {
            Pagination = pagination;
            SchoolId = schoolId;
            AcadmicYearId = acadmicYearId;
        }
    }
}
