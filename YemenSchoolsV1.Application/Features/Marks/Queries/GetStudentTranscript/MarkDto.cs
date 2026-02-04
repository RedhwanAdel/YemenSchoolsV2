namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentTranscript
{
    public class MarkDto
    {
        public Guid MarkId { get; set; }
        public required string SubjectName { get; set; }
        public required string AssessmentType { get; set; }
        public double Score { get; set; }
        public double MaxScore { get; set; }
    }
}
