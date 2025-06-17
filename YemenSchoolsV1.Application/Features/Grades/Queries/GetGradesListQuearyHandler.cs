using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Grades.Queries
{
	public class GetGradesListQuearyHandler : ResponseHandler, IRequestHandler<GetGradesListQueary, PaginatedResult<GetGradesListResponse>>
	{
		private readonly IGradeRepositry gradeRepositry;
		#region faild

		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public GetGradesListQuearyHandler(IGradeRepositry gradeRepositry, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.gradeRepositry = gradeRepositry;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;

		}

		#endregion
		public async Task<PaginatedResult<GetGradesListResponse>> Handle(GetGradesListQueary request, CancellationToken cancellationToken)
		{
			var result = await gradeRepositry.GetPagedAsync(
				  paginationQuery: request.Pagination,
				  predicate: x => x.Term.AcademicYear.Stage.SchoolId == request.SchoolId,
				  orderBy: x => x.OrderBy(s => s.Name));

			return PaginatedResult<GetGradesListResponse>.Success(mapper.Map<List<GetGradesListResponse>>(result.Data), result.TotalRecords, result.PageNumber, result.PageSize);
		}

	}
}
