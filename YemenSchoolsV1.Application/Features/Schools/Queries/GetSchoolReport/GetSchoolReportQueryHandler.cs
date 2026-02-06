using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolReport
{
    public class GetSchoolReportQueryHandler : ResponseHandler, IRequestHandler<GetSchoolReportQuery, Response<SchoolReportDto>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolReportQueryHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolReportDto>> Handle(GetSchoolReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _repository.GetSchoolReportAsync(request.Id);
            if (report == null) return NotFound<SchoolReportDto>();
            return Success(report);
        }
    }
}
