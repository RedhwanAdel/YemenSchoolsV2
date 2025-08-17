namespace YemenSchoolsV1.Application.Dto.Marks
{
    public class SectionMarkReportDto
    {
        public Guid SectionId { get; set; }
        public required string SectionName { get; set; }
        public Guid SubjectId { get; set; }
        public required string SubjectName { get; set; }

        // قائمة بملخص أداء كل طالب في المادة
        public required List<StudentPerformanceSummaryDto> StudentsSummary { get; set; } = new();
    }
}
