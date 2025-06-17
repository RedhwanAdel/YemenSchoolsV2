using YemenSchoolsV1.Domain.Commons;

namespace YemenSchoolsV1.Domain.Entities
{
	public class SchoolNews : GeneralLocalizableEntity
	{
		public Guid Id { get; set; }
		public Guid SchoolId { get; set; }
		public required string Title { get; set; } // عنوان التحديث
		public required string Description { get; set; } // وصف التحديث
		public string? MainPhoto { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now; // تاريخ إنشاء التحديث

		// العلاقة
		public School School { get; set; } = null!; // الربط بجدول المدرسة

		public ICollection<NewsPhoto> NewsPhotos { get; set; } = [];

	}
}
