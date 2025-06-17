namespace YemenSchoolsV1.Domain.Entities
{
	public class SchoolPhoto
	{
		public Guid Id { get; set; }
		public Guid SchoolId { get; set; }
		public required string PhotoUrl { get; set; }

		// العلاقة
		public School School { get; set; } = null!;
	}
}
