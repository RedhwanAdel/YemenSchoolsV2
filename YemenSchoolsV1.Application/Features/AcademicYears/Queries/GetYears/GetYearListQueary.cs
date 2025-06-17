using MediatR;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
	public class GetYearListQueary : IRequest<PaginatedResult<GetYearListResponse>>
	{
		public PaginationQuery Pagination { get; set; }
		public Guid SchoolId { get; set; }

		public GetYearListQueary(PaginationQuery pagination, Guid schoolId)
		{
			Pagination = pagination;
			SchoolId = schoolId;
		}
	}
}
