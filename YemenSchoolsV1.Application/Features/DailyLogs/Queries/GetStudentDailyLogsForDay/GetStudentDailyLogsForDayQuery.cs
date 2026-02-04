using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogsForDay
{
    public class GetStudentDailyLogsForDayQuery : IRequest<Response<List<DailyLogDto>>>
    {
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }

        public GetStudentDailyLogsForDayQuery(Guid studentId, DateTime date)
        {
            StudentId = studentId;
            Date = date;
        }
    }
}
