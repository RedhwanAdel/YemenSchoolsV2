using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Commons;

namespace YemenSchoolsV1.Domain.Entities
{
	public class Subject : GeneralLocalizableEntity, ISchoolEntity
	{
		public Guid Id { get; set; }

		public string NameAr { get; set; } = string.Empty;
		public string NameEn { get; set; } = string.Empty;

		public Guid SchoolId { get; set; }
		public School School { get; set; } = null!;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // وقت الإنشاء
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // آخر تحديث

		public ICollection<SubjectGrade> SubjectGrades { get; set; } = [];
		public ICollection<AssignedSubject> AssignedSubjects { get; set; } = [];

	}
}