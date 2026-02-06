using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommandHandler : ResponseHandler, IRequestHandler<CreateTermCommand, Response<string>>
    {
        private readonly ITermRepository _termRepository;
        private readonly IMapper _mapper;

        public CreateTermCommandHandler(ITermRepository termRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _termRepository = termRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
        {
            var termDomain = _mapper.Map<Term>(request);
            termDomain = await _termRepository.AddAsync(termDomain);
            if (termDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
