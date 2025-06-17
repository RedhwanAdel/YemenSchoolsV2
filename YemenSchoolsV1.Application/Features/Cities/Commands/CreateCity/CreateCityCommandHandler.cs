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
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : ResponseHandler, IRequestHandler<CreateCityCommand, Response<CreateCityResponse>>
    {
        #region faild

        private readonly ICityService cityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateCityCommandHandler(ICityService cityService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.cityService = cityService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;


        }
        #endregion


        public async Task<Response<CreateCityResponse>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityDomain = mapper.Map<City>(request);
            cityDomain = await cityService.CreateCityAsync(cityDomain);
            if (cityDomain == null)
            {
                return UnprocessableEntity<CreateCityResponse>();
            }

            var cityResponse = mapper.Map<CreateCityResponse>(cityDomain);
            return Created(cityResponse);

        }

    }
}
