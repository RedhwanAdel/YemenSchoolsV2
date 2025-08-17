namespace YemenSchoolsV1.Domain.Entities
{
    public class Mark
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid SectionSubjectId { get; set; }

        // الدرجة الممنوحة
        public double Score { get; set; }

        // الحد الأقصى للدرجة
        public double MaxScore { get; set; }

        // نوع التقييم (مثلاً: "الاختبار الأول"، "الواجب")
        public required string AssessmentType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Student Student { get; set; } = null!;
        public SectionSubject SectionSubject { get; set; } = null!;
    }
}
