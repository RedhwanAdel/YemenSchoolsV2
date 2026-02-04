using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetDailyLogById
{
    public class GetDailyLogByIdQueryHandler : IRequestHandler<GetDailyLogByIdQuery, Response<DailyLogDto>>
    {
        private readonly IDailyLogRepository _repository;

        public GetDailyLogByIdQueryHandler(IDailyLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<DailyLogDto>> Handle(GetDailyLogByIdQuery request, CancellationToken cancellationToken)
        {
            var dailyLog = await _repository.GetDailyLogByIdAsync(request.Id);
            if (dailyLog == null)
            {
                return new Response<DailyLogDto>("Daily Log not found") { StatusCode = System.Net.HttpStatusCode.NotFound, Succeeded = false };
            }

            var dto = new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered ?? "-",
                HomeworkAssigned = dailyLog.HomeworkAssigned ?? "-",
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SubjectName = dailyLog.SectionSubject?.GradeSubject?.Subject?.Name ?? "",
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            };

            return new Response<DailyLogDto>(dto);
        }
    }
}
