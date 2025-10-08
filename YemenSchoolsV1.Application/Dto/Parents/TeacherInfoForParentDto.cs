namespace YemenSchoolsV1.Application.Dto.Parents
{
    public class TeacherInfoForParentDto
    {
        public Guid TeacherId { get; set; }
        public Guid? UserId { get; set; }
        public required string TeacherName { get; set; }
        public string? TeacherPhoto { get; set; }
        public required string SchoolName { get; set; }
        public required string GradeName { get; set; }
        public required string SectionName { get; set; }
        public required string SubjectName { get; set; }
        public required string StudentName { get; set; }
    }
}
