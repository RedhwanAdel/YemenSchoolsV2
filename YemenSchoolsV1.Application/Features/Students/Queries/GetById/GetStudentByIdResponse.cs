using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetById
{
	public class GetStudentByIdResponse
	{
		public Guid Id { get; set; }
		public string RegisterNo { get; set; } = default!;
		public string NameEn { get; set; } = default!;
		public string NameAr { get; set; } = default!;
		public DateTime BirthDate { get; set; }
		public string? ProfileImage { get; set; }
		public Gender Gender { get; set; }
		public string? Nationality { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public bool IsActive { get; set; }

		// علاقات اختيارية (يمكن تضمينها إن أردت)
		public Guid SectionId { get; set; }
		public Guid? UserId { get; set; }
	}
}
