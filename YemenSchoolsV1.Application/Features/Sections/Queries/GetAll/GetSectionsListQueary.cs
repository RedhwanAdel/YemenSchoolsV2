using MediatR;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetAll
{
	public class GetSectionsListQueary : IRequest<PaginatedResult<GetSectionsListResponse>>
	{
		public PaginationQuery Pagination { get; set; }
		public Guid SchoolId { get; set; }

		public GetSectionsListQueary(PaginationQuery pagination, Guid schoolId)
		{
			Pagination = pagination;
			SchoolId = schoolId;
		}
	}
}
