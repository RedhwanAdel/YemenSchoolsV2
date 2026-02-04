namespace YemenSchoolsV1.Application.Features.Parents
{
    public class StudentWithSchoolInfoDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
    }
}
