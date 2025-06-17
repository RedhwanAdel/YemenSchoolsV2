using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetById
{
	public class GetTeacherDetailsResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public Gender Gender { get; set; }
		public DateTime HireDate { get; set; }
		public string Specialization { get; set; } = string.Empty;
		public string EmploymentStatus { get; set; } = string.Empty;
		public string ProfilePictureUrl { get; set; } = string.Empty;
		public Guid SchoolId { get; set; }
	}
}