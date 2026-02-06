using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesListQuearyHandler : ResponseHandler, IRequestHandler<GetCitiesListQueary, Response<List<GetCitiesListResponse>>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetCitiesListQuearyHandler(ICityRepository cityRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetCitiesListResponse>>> Handle(GetCitiesListQueary request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllAsync();
            var response = _mapper.Map<List<GetCitiesListResponse>>(cities);
            return Success(response);
        }
    }
}
