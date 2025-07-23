namespace YemenSchoolsV1.Application.Dto
{
    public class SchoolGradeDto
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid StageGradeId { get; set; }
        public string? StageName { get; set; }
        public string? GradeName { get; set; }
    }
}
