namespace YemenSchoolsV1.Application.Dto
{
    public class SectionSubjectInfoDto
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Guid GradeSubjectId { get; set; }
        public Guid TermId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SubjectId { get; set; }

        public string SubjectName { get; set; }
        public string TermName { get; set; }
        public string? TeacherName { get; set; }

    }
}
