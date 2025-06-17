using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
	public class Parent
	{
		public Guid Id { get; set; }

		public required string NameAr { get; set; }
		public string? NameEn { get; set; }

		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }

		public string? NationalId { get; set; }
		public Gender Gender { get; set; }

		public DateTime? BirthDate { get; set; }
		public string? JobTitle { get; set; }

		// ربطه بحساب المستخدم
		public Guid? UserId { get; set; }
		public AppUser User { get; set; } = null!;

		// علاقته بالأبناء (طلاب)
		public ICollection<ParentStudent> Students { get; set; } = [];

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
