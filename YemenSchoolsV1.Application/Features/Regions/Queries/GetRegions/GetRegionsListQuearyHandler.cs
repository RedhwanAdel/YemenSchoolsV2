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
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsListQuearyHandler : ResponseHandler, IRequestHandler<GetRegionsListQueary, Response<List<GetRegionsListResponse>>>
    {
        #region Fields
        private readonly IRegionService regionService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetRegionsListQuearyHandler(IRegionService regionService,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.regionService = regionService;
            _localizer = localizer;
            this.mapper = mapper;
        }
        #endregion

        public async Task<Response<List<GetRegionsListResponse>>> Handle(GetRegionsListQueary request, CancellationToken cancellationToken)
        {
            var regions = await regionService.GetAllRegionsAsync();
            var response = mapper.Map<List<GetRegionsListResponse>>(regions);
            foreach (var region in response) {
                region.countSchools = await regionService.GetAllSchoolCountAsync(region.Id);
            }
            return Success(response);
        }



    }
}
