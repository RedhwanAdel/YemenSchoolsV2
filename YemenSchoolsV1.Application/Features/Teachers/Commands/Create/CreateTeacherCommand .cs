using FinalProject.Application.Bases;
using MediatR;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Create
{
	public class CreateTeacherCommand : IRequest<Response<string>>
	{
		public string NameAr { get; set; } = string.Empty;
		public string NameEn { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public Gender Gender { get; set; }
		public DateTime HireDate { get; set; } = DateTime.UtcNow;
		public string Specialization { get; set; } = string.Empty;
		public string EmploymentStatus { get; set; } = string.Empty;
		public string ProfilePictureUrl { get; set; } = string.Empty;
		public Guid SchoolId { get; set; }
	}
}