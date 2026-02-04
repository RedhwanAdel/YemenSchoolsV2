using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogs
{
    public class GetStudentDailyLogsQuery : IRequest<Response<List<DailyLogDto>>>
    {
        public Guid StudentId { get; set; }

        public GetStudentDailyLogsQuery(Guid studentId)
        {
            StudentId = studentId;
        }
    }
}
