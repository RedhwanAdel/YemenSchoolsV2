using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Reports.Queries.GetSchoolReport
{
    public class GetSchoolReportQuery : IRequest<Response<FileResponse>>
    {
        public Guid SchoolId { get; set; }

        public GetSchoolReportQuery(Guid schoolId)
        {
            SchoolId = schoolId;
        }
    }
}
