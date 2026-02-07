using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Create
{
    public class CreateSectionSubjectCommandHandler : ResponseHandler, IRequestHandler<CreateSectionSubjectCommand, Response<string>>
    {
        private readonly ISectionSubjectRepository _repository;
        private readonly IMapper _mapper;

        public CreateSectionSubjectCommandHandler(
            ISectionSubjectRepository repository,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSectionSubjectCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SectionSubject>(request.Dto);
            var created = await _repository.AddAsync(entity);
            if (created == null)
                return UnprocessableEntity<string>("Failed to create SectionSubject.");

            return Created("SectionSubject created successfully.");
        }
    }
}
