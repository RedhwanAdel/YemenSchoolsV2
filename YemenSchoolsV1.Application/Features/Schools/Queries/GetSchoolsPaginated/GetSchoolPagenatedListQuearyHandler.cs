using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Bases;
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
        private readonly ISchoolRepository schoolRepository;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetSchoolPagenatedListQuearyHandler(ISchoolService schoolService, ISchoolRepository schoolRepository, ICityService cityService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.schoolRepository = schoolRepository;
            this.cityService = cityService;
            this.regionService = regionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }



        #endregion

        public async Task<PaginatedResponse<GetSchoolPagenatedListResponse>> Handle(GetSchoolPagenatedListQueary request, CancellationToken cancellationToken)
        {
            var queryable = schoolRepository.GetSchoolsWithCityAndRegionQueryable();

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

            // ------------------ الفرز (على مستوى الكيان) ------------------
            var sortDir = request.SortDirection?.Trim().ToLower() ?? "asc";
            
            switch (request.OrderBy)
            {
                case SchoolOrdering.Rating:
                    queryable = sortDir == "desc" 
                        ? queryable.OrderByDescending(x => x.Reviews.Average(r => (double?)r.Rating) ?? 0.0)
                        : queryable.OrderBy(x => x.Reviews.Average(r => (double?)r.Rating) ?? 0.0);
                    break;
                case SchoolOrdering.Name:
                    queryable = sortDir == "desc" 
                        ? queryable.OrderByDescending(x => x.NameAr)
                        : queryable.OrderBy(x => x.NameAr);
                    break;
                case SchoolOrdering.city:
                    queryable = sortDir == "desc" 
                        ? queryable.OrderByDescending(x => x.City.NameAr)
                        : queryable.OrderBy(x => x.City.NameAr);
                    break;
                case SchoolOrdering.region:
                    queryable = sortDir == "desc" 
                        ? queryable.OrderByDescending(x => x.Region.NameAr)
                        : queryable.OrderBy(x => x.Region.NameAr);
                    break;
                default:
                    queryable = queryable.OrderByDescending(x => x.Reviews.Average(r => (double?)r.Rating) ?? 0.0);
                    break;
            }

            // ------------------ Pagination & Projection ------------------
            // نقوم بالعد أولاً
            int count = await queryable.CountAsync();
            
            // ثم جلب الصفحة المطلوبة مع الـ Select
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var items = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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
                    AverageRating = s.Reviews.Average(r => (double?)r.Rating) ?? 0.0
                })
                .ToListAsync();

            var response = PaginatedResponse<GetSchoolPagenatedListResponse>.Success(items, count, pageNumber, pageSize);
            response.Meta = new { Count = items.Count };
            
            return response;
        }

    }
}
