using FinalProject.Application.Bases;
using MediatR;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Commands.Create
{
	public class CreateStudentCommand : IRequest<Response<string>>
	{
		public required string RegisterNo { get; set; }
		public required string NameEn { get; set; }
		public required string NameAr { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string? Nationality { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public Guid SectionId { get; set; }
		public Guid SchoolId { get; set; }

	}
}
