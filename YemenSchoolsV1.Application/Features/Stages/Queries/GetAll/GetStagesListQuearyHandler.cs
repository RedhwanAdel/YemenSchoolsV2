using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
    public class GetStagesListQuearyHandler : ResponseHandler, IRequestHandler<GetStagesListQueary, Response<List<GetStagesListResponse>>>
    {
        private readonly IStageRepository _stageRepository;
        private readonly IMapper _mapper;

        public GetStagesListQuearyHandler(IStageRepository stageRepository,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _stageRepository = stageRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetStagesListResponse>>> Handle(GetStagesListQueary request, CancellationToken cancellationToken)
        {
            var stagesDomain = await _stageRepository.GetAllAsync();
            var stages = _mapper.Map<List<GetStagesListResponse>>(stagesDomain);

            if (stages == null)
            {
                return NotFound<List<GetStagesListResponse>>();
            }
            return Success(stages);
        }
    }
}
