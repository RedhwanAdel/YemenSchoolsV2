using MediatR;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
	public class GetSchoolPagenatedListQueary : IRequest<PaginatedResponse<GetSchoolPagenatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public SchoolOrdering OrderBy { get; set; }
		public string? Search { get; set; }
		public Guid? CityId { get; set; }
		public Guid? RegionId { get; set; }
	}
}
