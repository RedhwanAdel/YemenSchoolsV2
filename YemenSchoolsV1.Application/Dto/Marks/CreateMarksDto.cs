namespace YemenSchoolsV1.Application.Dto.Marks
{
    // يمثل مجموعة الدرجات التي سيتم إدخالها
    public class CreateMarksDto
    {
        // معرف العلاقة بين الشعبة والمادة والمعلم
        public Guid SectionSubjectId { get; set; }
        public int MaxScore { get; set; }

        // نوع التقييم (مثلاً: "الاختبار الأول")
        public required string AssessmentType { get; set; }

        // قاموس يحتوي على معرف الطالب ودرجته
        public required Dictionary<Guid, double> StudentScores { get; set; }
    }
}
