using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Update
{
    public class EditSubjectCommandHandler : ResponseHandler, IRequestHandler<EditSubjectCommand, Response<string>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public EditSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var subjetDomain = _mapper.Map<Subject>(request);
            subjetDomain = await _subjectRepository.UpdateAsync(request.Id, subjetDomain);
            if (subjetDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
