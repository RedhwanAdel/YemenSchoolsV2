using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Commons;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
	public class Student : GeneralLocalizableEntity, ISchoolEntity
	{
		public Guid Id { get; set; }
		public string RegisterNo { get; set; }

		public string NameEn { get; set; }

		public string NameAr { get; set; }

		public DateTime BirthDate { get; set; }
		public string? ProfileImage { get; set; }

		public Gender Gender { get; set; }

		public string? Nationality { get; set; }

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }

		public string? Email { get; set; }
		public bool IsActive { get; set; } = true;

		public Guid? UserId { get; set; }
		public AppUser User { get; set; } = null!;

		public Guid SectionId { get; set; }
		public Section Section { get; set; } = null!;
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
		public Guid SchoolId { get; set; }

		public ICollection<ParentStudent> Parents { get; set; } = [];

	}
}
