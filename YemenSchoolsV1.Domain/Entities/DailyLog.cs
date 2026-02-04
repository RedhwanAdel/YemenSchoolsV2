namespace YemenSchoolsV1.Domain.Entities
{
    public class DailyLog
    {
        public Guid Id { get; set; }
        public string? LessonCovered { get; set; }
        public string? HomeworkAssigned { get; set; }
        public string? TeacherNotes { get; set; }
        public DateTime Date { get; set; }

        // المفاتيح الخارجية للربط مع الكيانات الأخرى
        public Guid SectionSubjectId { get; set; }
        public Guid TeacherId { get; set; }

        // خصائص التنقل (Navigation Properties)
        public SectionSubject SectionSubject { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
    }
}
