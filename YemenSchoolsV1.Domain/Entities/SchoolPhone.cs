namespace YemenSchoolsV1.Domain.Entities
{
	public class SchoolPhone
	{
		public Guid Id { get; set; }
		public Guid SchoolId { get; set; }
		public required string PhoneNumber { get; set; }


		public School School { get; set; } = null!;
	}
}
