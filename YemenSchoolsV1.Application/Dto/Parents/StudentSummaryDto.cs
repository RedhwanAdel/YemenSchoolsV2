namespace YemenSchoolsV1.Application.Dto.Parents
{
    public class StudentSummaryDto
    {
        public Guid StudentId { get; set; }
        public required string StudentName { get; set; }
        public required string RelationType { get; set; }
    }
}
