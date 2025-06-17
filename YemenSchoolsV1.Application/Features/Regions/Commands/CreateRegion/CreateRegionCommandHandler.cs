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
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommandHandler : ResponseHandler, IRequestHandler<CreateRegionCommand, Response<CreateRegionResponse>>
    {
        #region Fields
        private readonly IRegionService regionService;
        private readonly ICityService cityService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public CreateRegionCommandHandler(IRegionService regionService,ICityService cityService,
                                   IStringLocalizer<SharedResources> localizer,IMapper mapper):base(localizer)
        {
            this.regionService = regionService;
            this.cityService = cityService;
            _localizer = localizer;
            this.mapper = mapper;
        }
        #endregion

        public async Task<Response<CreateRegionResponse>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var city = await cityService.GetCityDetailsAsync(request.CityId);
            if(city == null)
            {
                return BadRequest<CreateRegionResponse>();
            }
            var regionDomain = mapper.Map<Region>(request);
            regionDomain = await regionService.CreateRegionAsync(regionDomain);
            if (regionDomain == null)
            {
                return UnprocessableEntity<CreateRegionResponse>();
            }

            var regionResponse = mapper.Map<CreateRegionResponse>(regionDomain);
            return Created(regionResponse);
        }
    }
}
