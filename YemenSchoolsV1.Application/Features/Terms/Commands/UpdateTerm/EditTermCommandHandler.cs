using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.UpdateTerm
{
    public class EditTermCommandHandler : ResponseHandler, IRequestHandler<EditTermCommand, Response<string>>
    {
        private readonly ITermRepository _termRepository;
        private readonly IMapper _mapper;

        public EditTermCommandHandler(ITermRepository termRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _termRepository = termRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditTermCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var termDomain = _mapper.Map<Term>(request);
            termDomain = await _termRepository.UpdateAsync(request.Id, termDomain);
            if (termDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
