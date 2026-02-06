using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.SetCurrentYear
{
    public class SetCurrentYearCommandHandler : ResponseHandler, IRequestHandler<SetCurrentYearCommand, Response<Guid>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public SetCurrentYearCommandHandler(IAcademicYearRepository academicYearRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Response<Guid>> Handle(SetCurrentYearCommand request, CancellationToken cancellationToken)
        {
            var result = await _academicYearRepository.SetCurrentYearAsync(request.SchoolId, request.AcademicYearId);
            
            if (result == null)
            {
                return NotFound<Guid>();
            }

            return Success(result.Value, SharedResourcesKeys.Update);
        }
    }
}
