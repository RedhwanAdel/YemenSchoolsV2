using MediatR;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Grades.Queries
{
	public class GetGradesListQueary : IRequest<PaginatedResult<GetGradesListResponse>>
	{
		public PaginationQuery Pagination { get; set; }
		public Guid SchoolId { get; set; }

		public GetGradesListQueary(PaginationQuery pagination, Guid schoolId)
		{
			Pagination = pagination;
			SchoolId = schoolId;
		}
	}
}
