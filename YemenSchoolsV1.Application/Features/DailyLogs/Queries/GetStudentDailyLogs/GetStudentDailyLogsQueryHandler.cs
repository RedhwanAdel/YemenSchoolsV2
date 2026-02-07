using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetStudentDailyLogs
{
    public class GetStudentDailyLogsQueryHandler : ResponseHandler, IRequestHandler<GetStudentDailyLogsQuery, Response<List<DailyLogDto>>>
    {
        private readonly IDailyLogRepository _repository;

        public GetStudentDailyLogsQueryHandler(
            IDailyLogRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<DailyLogDto>>> Handle(GetStudentDailyLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _repository.GetStudentDailyLogsAsync(request.StudentId);

            var dtos = logs.Select(dailyLog => new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SubjectName = dailyLog.SectionSubject?.GradeSubject?.Subject?.Name ?? "",
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            }).ToList();

            return Success(dtos);
        }
    }
}

