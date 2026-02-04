using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Reports.Queries.GetSchoolReport
{
    public class GetSchoolReportQueryHandler : IRequestHandler<GetSchoolReportQuery, Response<FileResponse>>
    {
        private readonly ISchoolRepository _schoolService; // Using existing Repo interface name
        private readonly ISchoolReportService _schoolReportService;

        public GetSchoolReportQueryHandler(ISchoolRepository schoolService, ISchoolReportService schoolReportService)
        {
            _schoolService = schoolService;
            _schoolReportService = schoolReportService;
        }

        public async Task<Response<FileResponse>> Handle(GetSchoolReportQuery request, CancellationToken cancellationToken)
        {
            var schoolDto = await _schoolService.GetSchoolReportAsync(request.SchoolId);

            if (schoolDto == null)
            {
                return new Response<FileResponse>("المدرسة غير موجودة.") { StatusCode = System.Net.HttpStatusCode.NotFound, Succeeded = false };
            }

            var pdfBytes = _schoolReportService.GenerateSchoolReport(schoolDto);
            var fileName = $"SchoolReport_{schoolDto.NameEn ?? "School"}.pdf";
            var fileResponse = new FileResponse(pdfBytes, "application/pdf", fileName);
            
            return new Response<FileResponse>(fileResponse);
        }
    }
}
