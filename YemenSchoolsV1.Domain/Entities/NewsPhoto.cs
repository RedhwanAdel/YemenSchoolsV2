namespace YemenSchoolsV1.Domain.Entities
{
	public class NewsPhoto
	{
		public Guid Id { get; set; } // المفتاح الأساسي GUID
		public Guid NewsId { get; set; } // المفتاح الخارجي GUID
		public required string PhotoUrl { get; set; } // رابط الصورة

		public DateTime UploadedDate { get; set; } // تاريخ رفع الصورة

		// العلاقة
		public SchoolNews SchoolNews { get; set; } = null!;
	}
}
