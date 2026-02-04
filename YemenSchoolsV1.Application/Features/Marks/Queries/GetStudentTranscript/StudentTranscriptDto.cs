namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    // يمثل كشف درجات الطالب
    public class StudentTranscriptDto
    {
        // معلومات الطالب الأساسية
        public Guid StudentId { get; set; }
        public required string StudentName { get; set; }
        public required string StudentSection { get; set; }

        // قائمة بجميع الدرجات
        public required List<MarkDto> Marks { get; set; } = new();

        // المعدل الإجمالي للطالب
        public double OverallAverage { get; set; }
    }


}
