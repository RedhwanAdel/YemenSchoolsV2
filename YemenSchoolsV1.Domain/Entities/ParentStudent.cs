namespace YemenSchoolsV1.Domain.Entities
{
	public class ParentStudent
	{
		public Guid ParentId { get; set; }
		public Parent Parent { get; set; } = null!;

		public Guid StudentId { get; set; }
		public Student Student { get; set; } = null!;

		public required string RelationType { get; set; } // أب، أم، ولي... إلخ
	}
}
