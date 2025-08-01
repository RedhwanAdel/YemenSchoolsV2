namespace YemenSchoolsV1.Domain.Entities
{
    public class ParentStudent
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
        public bool IsPrimaryContact { get; set; } = false; // Default to false, set to true if applicable

        public required string RelationType { get; set; } // أب، أم، ولي... إلخ
        public Parent Parent { get; set; } = null!;
        public Student Student { get; set; } = null!;

    }
}
