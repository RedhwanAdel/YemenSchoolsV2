namespace YemenSchoolsV1.Application.Dto.Students
{
    public class SectionSubjecUpdateDto
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Guid GradeSubjectId { get; set; }
        public Guid TermId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
