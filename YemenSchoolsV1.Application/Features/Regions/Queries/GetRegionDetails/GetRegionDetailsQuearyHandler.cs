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
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails
{
    public class GetRegionDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetRegionDetailsQueary, Response<GetRegionDetailsResponse>>
    {
        #region Fields
        private readonly IRegionService regionService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetRegionDetailsQuearyHandler(IRegionService regionService,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.regionService = regionService;
            _localizer = localizer;
            this.mapper = mapper;
        }
        public async Task<Response<GetRegionDetailsResponse>> Handle(GetRegionDetailsQueary request, CancellationToken cancellationToken)
        {
            var region = await regionService.GetRegionDetailsAsync(request.Id);
            if (region == null)
            {
                return NotFound<GetRegionDetailsResponse>();
            }
            var result = mapper.Map<GetRegionDetailsResponse>(region);
            return Success(result);
        }
        #endregion
    }
}
