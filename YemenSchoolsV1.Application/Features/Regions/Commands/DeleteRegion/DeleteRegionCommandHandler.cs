using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommandHandler : ResponseHandler, IRequestHandler<DeleteRegionCommand, Response<bool>>
    {
        private readonly IRegionRepository _regionRepository;

        public DeleteRegionCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IRegionRepository regionRepository) : base(stringLocalizer)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Response<bool>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetByIdAsync(request.Id);
            if (region == null)
            {
                return NotFound<bool>();
            }
            var deleted = await _regionRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
