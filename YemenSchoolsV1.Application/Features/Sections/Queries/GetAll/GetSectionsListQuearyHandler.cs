using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetAll
{
	public class GetSectionsListQuearyHandler : ResponseHandler, IRequestHandler<GetSectionsListQueary, PaginatedResult<GetSectionsListResponse>>
	{
		private readonly ISectionRepositry sectionRepositry;
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IMapper mapper;
		#endregion

		#region Constructors
		public GetSectionsListQuearyHandler(ISectionRepositry sectionRepositry,
								   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
		{
			this.sectionRepositry = sectionRepositry;
			_localizer = localizer;
			this.mapper = mapper;
		}

		#endregion
		public async Task<PaginatedResult<GetSectionsListResponse>> Handle(GetSectionsListQueary request, CancellationToken cancellationToken)
		{
			var result = await sectionRepositry.GetPagedAsync(
				paginationQuery: request.Pagination,
				predicate: x => x.Grade.Term.AcademicYear.Stage.SchoolId == request.SchoolId,
				orderBy: x => x.OrderBy(s => s.Name));

			return PaginatedResult<GetSectionsListResponse>.Success(mapper.Map<List<GetSectionsListResponse>>(result.Data), result.TotalRecords, result.PageNumber, result.PageSize);
		}
	}
}
