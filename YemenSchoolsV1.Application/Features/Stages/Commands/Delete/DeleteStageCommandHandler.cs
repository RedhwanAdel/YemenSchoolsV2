using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Delete
{
    public class DeleteStageCommandHandler : ResponseHandler, IRequestHandler<DeleteStageCommand, Response<bool>>
    {
        private readonly IStageRepository _stageRepository;

        public DeleteStageCommandHandler(IStageRepository stageRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _stageRepository = stageRepository;
        }

        public async Task<Response<bool>> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
        {
            var stageEntity = await _stageRepository.GetByIdAsync(request.Id);
            if (stageEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _stageRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
