using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Reports.Queries.GetSchoolReport
{
    public class GetSchoolReportQueryHandler : ResponseHandler, IRequestHandler<GetSchoolReportQuery, Response<FileResponse>>
    {
        private readonly ISchoolRepository _schoolService; // Using existing Repo interface name
        private readonly ISchoolReportService _schoolReportService;

        public GetSchoolReportQueryHandler(
            ISchoolRepository schoolService,
            ISchoolReportService schoolReportService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolService = schoolService;
            _schoolReportService = schoolReportService;
        }

        public async Task<Response<FileResponse>> Handle(GetSchoolReportQuery request, CancellationToken cancellationToken)
        {
            var schoolDto = await _schoolService.GetSchoolReportAsync(request.SchoolId);

            if (schoolDto == null)
            {
                return NotFound<FileResponse>("المدرسة غير موجودة.");
            }

            var pdfBytes = _schoolReportService.GenerateSchoolReport(schoolDto);
            var fileName = $"SchoolReport_{schoolDto.NameEn ?? "School"}.pdf";
            var fileResponse = new FileResponse(pdfBytes, "application/pdf", fileName);
            
            return Success(fileResponse);
        }
    }
}
