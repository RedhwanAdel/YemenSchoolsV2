namespace YemenSchoolsV1.Application.Dto
{
    public class SectionSummaryDto
    {
        public Guid SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public string GradeName { get; set; } = string.Empty;
        public int SubjectCount { get; set; }
    }
}
