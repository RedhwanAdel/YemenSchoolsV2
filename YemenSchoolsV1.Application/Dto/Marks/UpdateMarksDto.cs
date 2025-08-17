namespace YemenSchoolsV1.Application.Dto.Marks
{
    public class UpdateMarksDto
    {
        public Guid SectionSubjectId { get; set; }
        public required string AssessmentType { get; set; }
        public required Dictionary<Guid, double> StudentScores { get; set; }
    }
}
