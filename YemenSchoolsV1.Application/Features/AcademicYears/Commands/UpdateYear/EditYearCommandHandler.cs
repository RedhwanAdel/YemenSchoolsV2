using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear
{
    public class EditYearCommandHandler : ResponseHandler, IRequestHandler<EditYearCommand, Response<string>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IMapper _mapper;

        public EditYearCommandHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditYearCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var yearDomain = _mapper.Map<AcademicYear>(request);
            yearDomain = await _academicYearRepository.UpdateAsync(request.Id, yearDomain);
            if (yearDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
