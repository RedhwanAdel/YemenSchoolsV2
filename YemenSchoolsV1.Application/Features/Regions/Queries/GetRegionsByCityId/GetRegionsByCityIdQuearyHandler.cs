using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId
{
    public class GetRegionsByCityIdQuearyHandler : ResponseHandler, IRequestHandler<GetRegionsByCityIdQueary, Response<List<GetRegionsByCityIdResponse>>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetRegionsByCityIdQuearyHandler(IRegionRepository regionRepository, ICityRepository cityRepository, IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetRegionsByCityIdResponse>>> Handle(GetRegionsByCityIdQueary request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.CityId);
            if (city == null)
            {
                return BadRequest<List<GetRegionsByCityIdResponse>>();
            }

            var regions = await _regionRepository.GetRegionByCityIdIncludeAsync(request.CityId);
            if (regions == null)
            {
                return NotFound<List<GetRegionsByCityIdResponse>>();
            }
            var result = _mapper.Map<List<GetRegionsByCityIdResponse>>(regions);
            return Success(result);
        }
    }
}
