namespace YemenSchoolsV1.Application.Dto
{
    public class DailyLogCreateDto
    {
        public string LessonCovered { get; set; } = string.Empty;
        public string HomeworkAssigned { get; set; } = string.Empty;
        public string? TeacherNotes { get; set; }
        public Guid SectionSubjectId { get; set; }
    }
}
