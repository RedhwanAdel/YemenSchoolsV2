using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectCommandHandler : ResponseHandler, IRequestHandler<CreateSubjectCommand, Response<string>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectDomain = _mapper.Map<Subject>(request);
            subjectDomain = await _subjectRepository.AddAsync(subjectDomain);
            if (subjectDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
