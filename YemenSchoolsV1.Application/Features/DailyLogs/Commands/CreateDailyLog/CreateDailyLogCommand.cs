using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Commands.CreateDailyLog
{
    public class CreateDailyLogCommand : IRequest<Response<DailyLogDto>>
    {
        public string LessonCovered { get; set; } = string.Empty;
        public string HomeworkAssigned { get; set; } = string.Empty;
        public string? TeacherNotes { get; set; }
        public Guid SectionSubjectId { get; set; }
        
        // Injected by Controller
        public Guid TeacherId { get; set; }
    }
}
