namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects
{
    public class SectionSubjectDto
    {
        public Guid Id { get; set; }
        public required string SubjectName { get; set; }
        public Guid SectionId { get; set; }
        public required string SectionName { get; set; }
        public required string GradeName { get; set; }
    }
}
