using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Delete
{
    public class DeleteSectionCommandHandler : ResponseHandler, IRequestHandler<DeleteSectionCommand, Response<bool>>
    {
        private readonly ISectionRepository _repository;

        public DeleteSectionCommandHandler(
            ISectionRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id);
            if (!deleted)
                return NotFound<bool>("Section not found.");

            return Deleted<bool>();
        }
    }
}
