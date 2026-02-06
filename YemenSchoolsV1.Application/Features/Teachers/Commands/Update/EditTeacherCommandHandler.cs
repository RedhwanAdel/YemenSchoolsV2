using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Update
{
    public class EditTeacherCommandHandler : ResponseHandler, IRequestHandler<EditTeacherCommand, Response<string>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public EditTeacherCommandHandler(ITeacherRepository teacherRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var teacherDomain = _mapper.Map<Teacher>(request);
            teacherDomain = await _teacherRepository.UpdateAsync(request.Id, teacherDomain);

            if (teacherDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
