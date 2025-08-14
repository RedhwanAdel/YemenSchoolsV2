namespace YemenSchoolsV1.Application.Dto
{
    public class CreateSectionDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid AcademicYearId { get; set; }
        public Guid SchoolGradeId { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public int Capacity { get; set; }
    }
}
