using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommandHandler : ResponseHandler, IRequestHandler<DeleteTermCommand, Response<bool>>
    {
        private readonly ITermRepository _termRepository;

        public DeleteTermCommandHandler(ITermRepository termRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _termRepository = termRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
        {
            var termEntity = await _termRepository.GetByIdAsync(request.Id);
            if (termEntity == null)
            {
                return NotFound<bool>();
            }

            var deleted = await _termRepository.DeleteAsync(request.Id);
            if (!deleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
