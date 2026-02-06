using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionDetails
{
    public class GetRegionDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetRegionDetailsQueary, Response<GetRegionDetailsResponse>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public GetRegionDetailsQuearyHandler(IRegionRepository regionRepository, IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetRegionDetailsResponse>> Handle(GetRegionDetailsQueary request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetByIdAsync(request.Id);
            if (region == null)
            {
                return NotFound<GetRegionDetailsResponse>();
            }
            var result = _mapper.Map<GetRegionDetailsResponse>(region);
            return Success(result);
        }
    }
}
