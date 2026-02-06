using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Update
{
    public class EditStageCommandHandler : ResponseHandler, IRequestHandler<EditStageCommand, Response<string>>
    {
        private readonly IStageRepository _stageRepository;
        private readonly IMapper _mapper;

        public EditStageCommandHandler(IStageRepository stageRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _stageRepository = stageRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditStageCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var stageDomain = _mapper.Map<Stage>(request);
            stageDomain = await _stageRepository.UpdateAsync(request.Id, stageDomain);
            if (stageDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
