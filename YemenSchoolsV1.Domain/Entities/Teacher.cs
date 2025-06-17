using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Commons;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
	public class Teacher : GeneralLocalizableEntity, ISchoolEntity
	{
		public Guid Id { get; set; }

		public string NameAr { get; set; }
		public string NameEn { get; set; }

		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public Gender Gender { get; set; }

		public DateTime HireDate { get; set; } = DateTime.UtcNow;

		public Guid SchoolId { get; set; }
		public School School { get; set; } = null!;

		// التخصص (مثال: رياضيات، فيزياء، إلخ)
		public string Specialization { get; set; }

		// الحالة الوظيفية (مثال: نشط، إجازة، متقاعد)
		public string? EmploymentStatus { get; set; }

		public string? ProfilePictureUrl { get; set; }

		public Guid? UserId { get; set; }
		public AppUser User { get; set; } = null!;

		// العلاقة مع المواد المخصصة له
		public ICollection<AssignedSubject> AssignedSubjects { get; set; } = [];
	}
}