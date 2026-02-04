namespace YemenSchoolsV1.Application.Dto
{
    public class StageGradeDto
    {
        public Guid StageGradeId { get; set; }
        public string? StageName { get; set; }
        public string? GradeName { get; set; }
        public bool IsSelected { get; set; }

    }
}
