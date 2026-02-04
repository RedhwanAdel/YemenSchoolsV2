namespace YemenSchoolsV1.Application.Features.DailyLogs.Dto
{
    public class DailyLogDto
    {
        public Guid Id { get; set; }
        public string LessonCovered { get; set; } = string.Empty;
        public string HomeworkAssigned { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string? TeacherNotes { get; set; }

        public DateTime Date { get; set; }
        public Guid SectionSubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
