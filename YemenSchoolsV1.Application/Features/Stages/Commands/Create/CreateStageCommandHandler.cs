using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Create
{
    public class CreateStageCommandHandler : ResponseHandler, IRequestHandler<CreateStageCommand, Response<string>>
    {
        private readonly IStageRepository _stageRepository;
        private readonly IMapper _mapper;

        public CreateStageCommandHandler(IStageRepository stageRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _stageRepository = stageRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateStageCommand request, CancellationToken cancellationToken)
        {
            var stageDomain = _mapper.Map<Stage>(request);
            stageDomain = await _stageRepository.AddAsync(stageDomain);
            if (stageDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
