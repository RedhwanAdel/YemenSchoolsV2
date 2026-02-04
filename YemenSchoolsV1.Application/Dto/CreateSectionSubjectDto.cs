namespace YemenSchoolsV1.Application.Dto
{
    public class CreateSectionSubjectDto
    {
        public Guid SectionId { get; set; }
        public Guid GradeSubjectId { get; set; }
        public Guid TermId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
