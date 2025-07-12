using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
	public class GetSchoolPagenatedListQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolPagenatedListQueary, PaginatedResponse<GetSchoolPagenatedListResponse>>
	{
		#region faild

		private readonly ISchoolService schoolService;
		private readonly ISchoolRepositry schoolRepositry;
		private readonly ICityService cityService;
		private readonly IRegionService regionService;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public GetSchoolPagenatedListQuearyHandler(ISchoolService schoolService, ISchoolRepositry schoolRepositry, ICityService cityService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.schoolService = schoolService;
			this.schoolRepositry = schoolRepositry;
			this.cityService = cityService;
			this.regionService = regionService;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;

		}



		#endregion

		public async Task<PaginatedResponse<GetSchoolPagenatedListResponse>> Handle(GetSchoolPagenatedListQueary request, CancellationToken cancellationToken)
		{
			var queryable = schoolRepositry.GetSchoolsWithCityAndRegionQueryable();


			if (request.CityId != Guid.Empty && request.CityId != null)
			{
				queryable = queryable.Where(x => x.CityId == request.CityId);
			}
			if (request.RegionId != Guid.Empty && request.RegionId != null)
			{
				queryable = queryable.Where(x => x.RegionId == request.RegionId);
			}
			if (!string.IsNullOrEmpty(request.Search))
			{
				queryable = queryable.Where(x => x.NameEn.ToLower().Contains(request.Search.ToLower()));
			}
			if (request.Type.HasValue)
			{
				queryable = queryable.Where(x => x.SchoolType == request.Type.Value);
			}

			if (request.Levels.HasValue)
			{
				queryable = queryable.Where(x => (x.SchoolLevel & request.Levels.Value) != 0); // [Flags] filter
			}

			if (request.Gender.HasValue)
			{
				queryable = queryable.Where(x => x.GenderType == request.Gender.Value);
			}

			switch (request.SortDirection?.Trim().ToLower())
			{
				case "asc":
					queryable = request.OrderBy switch
					{
						SchoolOrdering.Name => queryable.OrderBy(x => x.NameEn),
						SchoolOrdering.city => queryable.OrderBy(x => x.City.NameEn),
						SchoolOrdering.region => queryable.OrderBy(x => x.Region.NameEn),
						_ => queryable.OrderBy(x => x.NameEn)
					};
					break;

				case "desc":
					queryable = request.OrderBy switch
					{
						SchoolOrdering.Name => queryable.OrderByDescending(x => x.NameEn),
						SchoolOrdering.city => queryable.OrderByDescending(x => x.City.NameEn),
						SchoolOrdering.region => queryable.OrderByDescending(x => x.Region.NameEn),
						_ => queryable.OrderByDescending(x => x.NameEn)
					};
					break;

				default:
					queryable = queryable.OrderBy(x => x.NameEn);
					break;
			}

			var PaginatedList = await mapper.ProjectTo<GetSchoolPagenatedListResponse>(queryable).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
			return PaginatedList;

		}

	}
}
