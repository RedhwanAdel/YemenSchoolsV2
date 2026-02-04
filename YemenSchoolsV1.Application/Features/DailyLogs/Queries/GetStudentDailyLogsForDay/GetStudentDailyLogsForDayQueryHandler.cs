using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogsForDay
{
    public class GetStudentDailyLogsForDayQueryHandler : IRequestHandler<GetStudentDailyLogsForDayQuery, Response<List<DailyLogDto>>>
    {
        private readonly IDailyLogRepository _repository;

        public GetStudentDailyLogsForDayQueryHandler(IDailyLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<DailyLogDto>>> Handle(GetStudentDailyLogsForDayQuery request, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetStudentDailyLogsForDayAsync(request.StudentId, request.Date);

            var dtos = logs.Select(dailyLog => new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SubjectName = dailyLog.SectionSubject?.GradeSubject?.Subject?.Name ?? "", // Safe navigation
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            }).ToList();

            return new Response<List<DailyLogDto>>(dtos);
        }
    }
}
