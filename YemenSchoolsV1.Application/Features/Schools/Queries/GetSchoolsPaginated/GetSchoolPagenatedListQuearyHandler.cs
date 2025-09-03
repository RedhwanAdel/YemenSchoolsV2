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

            // ------------------ الفلترة ------------------
            if (request.CityId.HasValue && request.CityId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.CityId == request.CityId);
            }

            if (request.RegionId.HasValue && request.RegionId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.RegionId == request.RegionId);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                var searchLower = request.Search.ToLower();
                queryable = queryable.Where(x => x.NameAr.ToLower().Contains(searchLower));
            }

            if (request.Type.HasValue)
            {
                queryable = queryable.Where(x => x.SchoolType == request.Type.Value);
            }
            if (request.CurriculumType.HasValue)
            {
                queryable = queryable.Where(x => x.CurriculumType == request.CurriculumType.Value);
            }

            if (request.Levels.HasValue)
            {
                queryable = queryable.Where(x => (x.SchoolLevel & request.Levels.Value) != 0);
            }


            if (request.Gender.HasValue)
            {
                queryable = queryable.Where(x => x.GenderType == request.Gender.Value);
            }

            // ------------------ Select إلى DTO ------------------
            var dtoQueryable = queryable
                .Select(s => new GetSchoolPagenatedListResponse
                {
                    Id = s.Id,
                    Name = s.NameAr,
                    Logo = s.Logo,
                    SchoolType = s.SchoolType.ToString(),
                    GenderType = s.GenderType.ToString(),
                    City = s.City.NameAr,
                    Region = s.Region.NameAr,
                    CoverImage = s.CoverImage,
                    MainPhone = s.MainPhone,
                    SchoolLevel = s.SchoolLevel.ToString(),
                    CurriculumType = s.CurriculumType.ToString(),
                    AverageRating = s.Reviews.Any() ? s.Reviews.Average(r => r.Rating) : 0.0
                });

            // ------------------ الفرز ------------------
            switch (request.SortDirection?.Trim().ToLower())
            {
                case "asc":
                    dtoQueryable = request.OrderBy switch
                    {
                        SchoolOrdering.Rating => dtoQueryable.OrderBy(x => x.AverageRating),
                        SchoolOrdering.Name => dtoQueryable.OrderBy(x => x.Name),
                        SchoolOrdering.city => dtoQueryable.OrderBy(x => x.City),
                        SchoolOrdering.region => dtoQueryable.OrderBy(x => x.Region),
                        _ => dtoQueryable.OrderBy(x => x.Name)
                    };
                    break;

                case "desc":
                    dtoQueryable = request.OrderBy switch
                    {
                        SchoolOrdering.Rating => dtoQueryable.OrderByDescending(x => x.AverageRating),
                        SchoolOrdering.Name => dtoQueryable.OrderByDescending(x => x.Name),
                        SchoolOrdering.city => dtoQueryable.OrderByDescending(x => x.City),
                        SchoolOrdering.region => dtoQueryable.OrderByDescending(x => x.Region),
                        _ => dtoQueryable.OrderByDescending(x => x.Name)
                    };
                    break;

                default:
                    dtoQueryable = dtoQueryable.OrderBy(x => x.AverageRating);
                    break;
            }

            // ------------------ Pagination ------------------
            var paginatedList = await dtoQueryable
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;
        }

    }
}
