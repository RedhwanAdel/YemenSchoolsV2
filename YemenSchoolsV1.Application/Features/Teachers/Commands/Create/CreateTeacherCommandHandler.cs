using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Create
{
    public class CreateTeacherCommandHandler : ResponseHandler, IRequestHandler<CreateTeacherCommand, Response<string>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public CreateTeacherCommandHandler(ITeacherRepository teacherRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherDomain = _mapper.Map<Teacher>(request);
            teacherDomain = await _teacherRepository.AddAsync(teacherDomain);
            if (teacherDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
