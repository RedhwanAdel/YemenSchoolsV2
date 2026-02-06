using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommandHandler : ResponseHandler, IRequestHandler<CreateRegionCommand, Response<CreateRegionResponse>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CreateRegionCommandHandler(IRegionRepository regionRepository, ICityRepository cityRepository, IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateRegionResponse>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.CityId);
            if (city == null)
            {
                return BadRequest<CreateRegionResponse>();
            }
            var regionDomain = _mapper.Map<Region>(request);
            regionDomain = await _regionRepository.AddAsync(regionDomain);
            if (regionDomain == null)
            {
                return UnprocessableEntity<CreateRegionResponse>();
            }

            var regionResponse = _mapper.Map<CreateRegionResponse>(regionDomain);
            return Created(regionResponse);
        }
    }
}
