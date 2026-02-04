namespace YemenSchoolsV1.Domain.Entities
{
    public class Stage
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        // Navigation property 

        public ICollection<StageGrade> StageGrades { get; set; } = [];

    }
}
