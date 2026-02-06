using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetCurrentYearId
{
    public class GetCurrentYearIdQueryHandler : ResponseHandler, IRequestHandler<GetCurrentYearIdQuery, Response<Guid>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public GetCurrentYearIdQueryHandler(IAcademicYearRepository academicYearRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Response<Guid>> Handle(GetCurrentYearIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _academicYearRepository.GetCurrentYearIdAsync(request.SchoolId);
            
            if (result == null)
            {
                return NotFound<Guid>();
            }

            return Success(result.Value);
        }
    }
}
