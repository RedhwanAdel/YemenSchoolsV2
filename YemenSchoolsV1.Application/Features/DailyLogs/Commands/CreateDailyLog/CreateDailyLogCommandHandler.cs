using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Commands.CreateDailyLog
{
    public class CreateDailyLogCommandHandler : ResponseHandler, IRequestHandler<CreateDailyLogCommand, Response<DailyLogDto>>
    {
        private readonly IDailyLogRepository _repository;

        public CreateDailyLogCommandHandler(
            IDailyLogRepository repository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<DailyLogDto>> Handle(CreateDailyLogCommand request, CancellationToken cancellationToken)
        {
            var dailyLog = new DailyLog
            {
                Id = Guid.NewGuid(),
                LessonCovered = request.LessonCovered,
                HomeworkAssigned = request.HomeworkAssigned,
                TeacherNotes = request.TeacherNotes,
                Date = DateTime.UtcNow,
                SectionSubjectId = request.SectionSubjectId,
                TeacherId = request.TeacherId
            };

            await _repository.AddDailyLogAsync(dailyLog);

            var logToReturn = new DailyLogDto
            {
                Id = dailyLog.Id,
                LessonCovered = dailyLog.LessonCovered,
                HomeworkAssigned = dailyLog.HomeworkAssigned,
                TeacherNotes = dailyLog.TeacherNotes,
                Date = dailyLog.Date,
                SectionSubjectId = dailyLog.SectionSubjectId,
                TeacherId = dailyLog.TeacherId
            };

            return Created(logToReturn, "Created Successfully");
        }
    }
}
