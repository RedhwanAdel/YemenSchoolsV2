using MediatR;
using YemenSchoolsV1.Application.Bases.Models;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
	public class GetSchoolPagenatedListQueary : BasePaginatedQuery, IRequest<PaginatedResponse<GetSchoolPagenatedListResponse>>
	{

		public SchoolOrdering OrderBy { get; set; }
		public Guid? CityId { get; set; }
		public Guid? RegionId { get; set; }
		public SchoolType? Type { get; set; }
		public SchoolLevel? Levels { get; set; }
		public GenderType? Gender { get; set; }
	}
}
