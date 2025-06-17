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

namespace YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity
{
    public class EditCityCommandHandler:ResponseHandler,IRequestHandler<EditCityCommand,Response<EditCityResponse>>
    {
        #region faild

        private readonly ICityService cityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public EditCityCommandHandler(ICityService cityService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.cityService = cityService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

       

        public async Task<Response<EditCityResponse>> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<EditCityResponse>();
            }
            var cityDomain = mapper.Map<City>(request);
            cityDomain = await cityService.EditCityAsync(request.Id,cityDomain);
            if (cityDomain == null)
            {
                return UnprocessableEntity<EditCityResponse>();
            }

            var cityResponse = mapper.Map<EditCityResponse>(cityDomain);
            return Success<EditCityResponse>(cityResponse);
        }
        #endregion
    }
}
