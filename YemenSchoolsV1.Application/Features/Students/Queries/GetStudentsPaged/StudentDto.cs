using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsPaged
{
	public class StudentDto
	{
		public Guid Id { get; set; }
		public string RegisterNo { get; set; } = default!;
		public required string NameEn { get; set; }

		public string? ProfileImage { get; set; }
		public Gender Gender { get; set; }

	}
}
