namespace YemenSchoolsV1.Application.Dto
{
    public class SectionByGradeAndYearDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ClassTeacherName { get; set; }
        public string? GradeName { get; set; }

        public int Capacity { get; set; }
    }
}
