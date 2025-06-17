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

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetCityDetailsQueary, Response<GetCityDetailsResponse>>
    {
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        public GetCityDetailsQuearyHandler(IStringLocalizer<SharedResources> stringLocalizer,ICityService cityService,IMapper mapper) : base(stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
            this.cityService = cityService;
            this.mapper = mapper;
        }

        public async Task<Response<GetCityDetailsResponse>> Handle(GetCityDetailsQueary request, CancellationToken cancellationToken)
        {
            var city = await cityService.GetCityDetailsAsync(request.Id);
            if (city == null)
            {
                return NotFound<GetCityDetailsResponse>();
            }
            var result = mapper.Map<GetCityDetailsResponse>(city);
            return Success(result);
        }
    }
}
