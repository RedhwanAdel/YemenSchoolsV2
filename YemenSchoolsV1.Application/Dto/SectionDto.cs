namespace YemenSchoolsV1.Application.Dto
{
    public class SectionDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid SchoolGradeId { get; set; }
        public int Capacity { get; set; }
    }
}
