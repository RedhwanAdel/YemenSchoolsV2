using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : ResponseHandler, IRequestHandler<CreateCityCommand, Response<CreateCityResponse>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateCityResponse>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityDomain = _mapper.Map<City>(request);
            cityDomain = await _cityRepository.AddAsync(cityDomain);
            if (cityDomain == null)
            {
                return UnprocessableEntity<CreateCityResponse>();
            }

            var cityResponse = _mapper.Map<CreateCityResponse>(cityDomain);
            return Created(cityResponse);
        }
    }
}
