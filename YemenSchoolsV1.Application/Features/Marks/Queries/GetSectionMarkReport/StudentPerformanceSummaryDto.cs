namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetSectionMarkReport
{
    public class StudentPerformanceSummaryDto
    {
        public Guid StudentId { get; set; }
        public required string StudentName { get; set; }

        // قاموس يحتوي على نوع التقييم ودرجة الطالب
        public required Dictionary<string, double> AssessmentScores { get; set; } = new();

        // إجمالي درجة الطالب في المادة
        public double TotalScore { get; set; }
    }
}
