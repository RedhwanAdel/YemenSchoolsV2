using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
    public class GetSchoolPagenatedListQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolPagenatedListQueary, PaginatedResponse<GetSchoolPagenatedListResponse>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetSchoolPagenatedListQuearyHandler(ISchoolService schoolService, ICityService cityService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.cityService = cityService;
            this.regionService = regionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }



        #endregion

        public async Task<PaginatedResponse<GetSchoolPagenatedListResponse>> Handle(GetSchoolPagenatedListQueary request, CancellationToken cancellationToken)
        {
            var FilterQuery = schoolService.FilterSchoolPaginatedQuerable(request.OrderBy, request.Search,request.CityId,request.RegionId);

            var PaginatedList = await mapper.ProjectTo<GetSchoolPagenatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber,request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;

        }

    }
}
