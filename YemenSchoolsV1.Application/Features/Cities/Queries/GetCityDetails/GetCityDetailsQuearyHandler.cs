using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails
{
    public class GetCityDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetCityDetailsQueary, Response<GetCityDetailsResponse>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetCityDetailsQuearyHandler(IStringLocalizer<SharedResources> stringLocalizer, ICityRepository cityRepository, IMapper mapper) : base(stringLocalizer)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetCityDetailsResponse>> Handle(GetCityDetailsQueary request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.Id);
            if (city == null)
            {
                return NotFound<GetCityDetailsResponse>();
            }
            var result = _mapper.Map<GetCityDetailsResponse>(city);
            return Success(result);
        }
    }
}
