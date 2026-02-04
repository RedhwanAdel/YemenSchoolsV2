using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetCurrentYearId
{
    public class GetCurrentYearIdQueryHandler : IRequestHandler<GetCurrentYearIdQuery, Response<Guid>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public GetCurrentYearIdQueryHandler(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Response<Guid>> Handle(GetCurrentYearIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _academicYearRepository.GetCurrentYearIdAsync(request.SchoolId);
            
            if (result == null)
            {
                return new Response<Guid>("No current academic year found for this school.") { StatusCode = System.Net.HttpStatusCode.NotFound, Succeeded = false };
            }

            return new Response<Guid>(result.Value, "Current academic year ID retrieved successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true };
        }
    }
}
