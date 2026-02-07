using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetDailyLogById
{
    public class GetDailyLogByIdQueryHandler : ResponseHandler, IRequestHandler<GetDailyLogByIdQuery, Response<DailyLogDto>>
    {
        private readonly IDailyLogRepository _repository;

        public GetDailyLogByIdQueryHandler(
            IDailyLogRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<DailyLogDto>> Handle(GetDailyLogByIdQuery request, CancellationToken cancellationToken)
        {
            var dailyLog = await _repository.GetDailyLogByIdAsync(request.Id);
            if (dailyLog == null)
            {
                return NotFound<DailyLogDto>("Daily Log not found");
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

            return Success(dto);
        }
    }
}

