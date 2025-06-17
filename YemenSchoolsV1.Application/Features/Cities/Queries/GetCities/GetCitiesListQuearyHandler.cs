using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesListQuearyHandler : ResponseHandler, IRequestHandler<GetCitiesListQueary, Response<List<GetCitiesListResponse>>>
    {

        #region faild

        private readonly ICityService cityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetCitiesListQuearyHandler(ICityService cityService,IMapper mapper,IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer)
        {
            this.cityService = cityService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion


        #region handler
        public async Task<Response<List<GetCitiesListResponse>>> Handle(GetCitiesListQueary request, CancellationToken cancellationToken)
        {
            var cities = await cityService.GetAllCitiesAsync();
            var response = mapper.Map<List<GetCitiesListResponse>>(cities);
            return Success(response);
        }
        #endregion

    }
}
