using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.UpdateSchool
{
    public class EditSchoolForAdminCommandHandler : ResponseHandler, IRequestHandler<EditSchoolForAdminCommand, Response<string>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public EditSchoolForAdminCommandHandler(ISchoolRepository schoolRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditSchoolForAdminCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var schoolDomain = _mapper.Map<School>(request);
            schoolDomain = await _schoolRepository.UpdateAsync(request.Id, schoolDomain);
            if (schoolDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}

