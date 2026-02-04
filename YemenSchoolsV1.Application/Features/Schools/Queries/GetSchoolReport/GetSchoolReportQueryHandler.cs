using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolReport
{
    public class GetSchoolReportQueryHandler : IRequestHandler<GetSchoolReportQuery, Response<SchoolReportDto>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolReportQueryHandler(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolReportDto>> Handle(GetSchoolReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _repository.GetSchoolReportAsync(request.Id);
            if (report == null) return new Response<SchoolReportDto>("School not found") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.NotFound };
            return new Response<SchoolReportDto>(report);
        }
    }
}
