namespace YemenSchoolsV1.Domain.Entities
{
	public class SchoolRating
	{
		public Guid Id { get; set; }
		public School School { get; set; } = null!;

	}
}
