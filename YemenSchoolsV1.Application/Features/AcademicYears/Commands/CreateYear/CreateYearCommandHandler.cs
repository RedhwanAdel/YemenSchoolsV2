using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear
{
    public class CreateYearCommandHandler : ResponseHandler, IRequestHandler<CreateYearCommand, Response<string>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IMapper _mapper;

        public CreateYearCommandHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateYearCommand request, CancellationToken cancellationToken)
        {
            var yearDomain = _mapper.Map<AcademicYear>(request);
            yearDomain = await _academicYearRepository.AddAsync(yearDomain);
            if (yearDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
