using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogsForDay
{
    public class GetStudentDailyLogsForDayQueryHandler : ResponseHandler, IRequestHandler<GetStudentDailyLogsForDayQuery, Response<List<DailyLogDto>>>
    {
        private readonly IDailyLogRepository _repository;

        public GetStudentDailyLogsForDayQueryHandler(
            IDailyLogRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
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

            return Success(dtos);
        }
    }
}

