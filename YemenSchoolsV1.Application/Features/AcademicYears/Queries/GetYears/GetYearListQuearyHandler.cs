using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
	public class GetYearListQuearyHandler : ResponseHandler, IRequestHandler<GetYearListQueary, PaginatedResult<GetYearListResponse>>
	{
		private readonly IAcademicYearRepository academicYearRepository;
		#region faild

		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public GetYearListQuearyHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.academicYearRepository = academicYearRepository;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;
		}

		#endregion
		public async Task<PaginatedResult<GetYearListResponse>> Handle(GetYearListQueary request, CancellationToken cancellationToken)
		{
			var result = await academicYearRepository.GetPagedAsync(
				paginationQuery: request.Pagination,
				predicate: x => x.Stage.SchoolId == request.SchoolId,
				orderBy: x => x.OrderBy(s => s.Name));

			return PaginatedResult<GetYearListResponse>.Success(mapper.Map<List<GetYearListResponse>>(result.Data), result.TotalRecords, result.PageNumber, result.PageSize);

		}
	}
}
