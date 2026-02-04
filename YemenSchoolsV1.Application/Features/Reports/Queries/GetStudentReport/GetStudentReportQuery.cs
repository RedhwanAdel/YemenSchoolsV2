using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Reports.Queries.GetStudentReport
{
    public class GetStudentReportQuery : IRequest<Response<FileResponse>>
    {
        public Guid StudentId { get; set; }

        public GetStudentReportQuery(Guid studentId)
        {
            StudentId = studentId;
        }
    }
}
