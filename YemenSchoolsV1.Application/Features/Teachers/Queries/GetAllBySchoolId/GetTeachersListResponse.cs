namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId
{
	public class GetTeachersListResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string EmploymentStatus { get; set; } = string.Empty;
		public string? ProfilePictureUrl { get; set; }
		public Guid SchoolId { get; set; }
	}
}