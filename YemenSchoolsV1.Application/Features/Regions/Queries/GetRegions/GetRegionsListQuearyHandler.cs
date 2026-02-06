using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsListQuearyHandler : ResponseHandler, IRequestHandler<GetRegionsListQueary, Response<List<GetRegionsListResponse>>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public GetRegionsListQuearyHandler(IRegionRepository regionRepository, IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetRegionsListResponse>>> Handle(GetRegionsListQueary request, CancellationToken cancellationToken)
        {
            var regions = await _regionRepository.getAllRegionIncludeAsync();
            var response = _mapper.Map<List<GetRegionsListResponse>>(regions);
            foreach (var region in response)
            {
                region.countSchools = await _regionRepository.GetSchoolCount(region.Id);
            }
            return Success(response);
        }
    }
}
