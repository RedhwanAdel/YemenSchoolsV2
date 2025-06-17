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

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolCommand, Response<CreateSchoolResponse>>
    {

        #region faild

        private readonly ISchoolService schoolService;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateSchoolCommandHandler(ISchoolService schoolService,ICityService cityService,IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.cityService = cityService;
            this.regionService = regionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }


        #endregion


        public async Task<Response<CreateSchoolResponse>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            var region = await regionService.GetRegionDetailsAsync(request.RegionId);
            if (region == null) return BadRequest<CreateSchoolResponse>();
            var city =await  cityService.GetCityDetailsAsync(request.CityId);
            if (city == null) return BadRequest<CreateSchoolResponse>();

            var schoolDomain = mapper.Map<School>(request);
            schoolDomain = await schoolService.CreateSchoolAsync(schoolDomain);
            if (schoolDomain == null)
            {
                return UnprocessableEntity<CreateSchoolResponse>();
            }

            var schoolResponse = mapper.Map<CreateSchoolResponse>(schoolDomain);
            return Created(schoolResponse);
        }


    }
}
