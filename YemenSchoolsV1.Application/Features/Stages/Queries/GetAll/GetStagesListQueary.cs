using MediatR;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
	public class GetStagesListQueary : IRequest<PaginatedResult<GetStagesListResponse>>
	{
		public PaginationQuery Pagination { get; set; }
		public Guid SchoolId { get; set; }

		public GetStagesListQueary(PaginationQuery pagination, Guid schoolId)
		{
			Pagination = pagination;
			SchoolId = schoolId;
		}
	}
}
