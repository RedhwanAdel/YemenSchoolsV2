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
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails;
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId
{
    public class GetRegionsByCityIdQuearyHandler : ResponseHandler, IRequestHandler<GetRegionsByCityIdQueary, Response<List<GetRegionsByCityIdResponse>>>
    {
        #region Fields
        private readonly IRegionService regionService;
        private readonly ICityService cityService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetRegionsByCityIdQuearyHandler(IRegionService regionService,ICityService cityService,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.regionService = regionService;
            this.cityService = cityService;
            _localizer = localizer;
            this.mapper = mapper;
        }

        public async Task<Response<List<GetRegionsByCityIdResponse>>> Handle(GetRegionsByCityIdQueary request, CancellationToken cancellationToken)
        {
            var city = await cityService.GetCityDetailsAsync(request.CityId);
            if (city == null)
            {
                return BadRequest<List<GetRegionsByCityIdResponse>>();
            }

            var regions = await regionService.GetAllRegionsByCityIdAsync(request.CityId);
            if (regions == null)
            {
                return NotFound<List<GetRegionsByCityIdResponse>>();
            }
            var result = mapper.Map<List<GetRegionsByCityIdResponse>>(regions);
            return Success(result);
        }
        #endregion

    }
}
