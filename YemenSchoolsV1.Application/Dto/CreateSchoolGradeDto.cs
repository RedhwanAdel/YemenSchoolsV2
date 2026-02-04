namespace YemenSchoolsV1.Application.Dto
{
    public class CreateSchoolGradeDto
    {
        public Guid SchoolId { get; set; }
        public List<Guid> StageGradeIds { get; set; } = [];

    }
}
