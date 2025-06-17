using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetAll
{
	public class GetTermsListQuearyHandler : ResponseHandler, IRequestHandler<GetTermsListQueary, PaginatedResult<GetTermsListResponse>>
	{
		#region Fields
		private readonly ITermRepositry termRepositry;
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IMapper mapper;
		#endregion

		#region Constructors
		public GetTermsListQuearyHandler(ITermRepositry termRepositry,
								   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
		{
			this.termRepositry = termRepositry;
			_localizer = localizer;
			this.mapper = mapper;
		}

		#endregion
		public async Task<PaginatedResult<GetTermsListResponse>> Handle(GetTermsListQueary request, CancellationToken cancellationToken)
		{
			var result = await termRepositry.GetPagedAsync(
				paginationQuery: request.Pagination,
				predicate: x => x.AcademicYear.Stage.SchoolId == request.SchoolId,
				orderBy: x => x.OrderBy(s => s.Name));

			return PaginatedResult<GetTermsListResponse>.Success(mapper.Map<List<GetTermsListResponse>>(result.Data), result.TotalRecords, result.PageNumber, result.PageSize);
		}
	}
}
