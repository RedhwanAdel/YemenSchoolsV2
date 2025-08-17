namespace YemenSchoolsV1.Application.Dto.Marks
{
    public class SectionSubjectDto
    {
        public Guid Id { get; set; }
        public required string SubjectName { get; set; }
        public Guid SectionId { get; set; }
        public required string SectionName { get; set; }
    }
}
