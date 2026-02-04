using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.SetCurrentYear
{
    public class SetCurrentYearCommandHandler : IRequestHandler<SetCurrentYearCommand, Response<Guid>>
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public SetCurrentYearCommandHandler(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<Response<Guid>> Handle(SetCurrentYearCommand request, CancellationToken cancellationToken)
        {
            var result = await _academicYearRepository.SetCurrentYearAsync(request.SchoolId, request.AcademicYearId);
            
            if (result == null)
            {
                return new Response<Guid>("Academic year not found or school has no years.") { StatusCode = System.Net.HttpStatusCode.NotFound, Succeeded = false };
            }

            return new Response<Guid>(result.Value, "Current academic year set successfully.") { StatusCode = System.Net.HttpStatusCode.OK, Succeeded = true };
        }
    }
}
