namespace YemenSchoolsV1.Application.Dto
{
    public class SectionByGradeAndYearDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
