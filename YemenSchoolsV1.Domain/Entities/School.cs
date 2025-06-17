using YemenSchoolsV1.Domain.Commons;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Domain.Entities
{
	public class School : GeneralLocalizableEntity
	{
		public Guid Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string? DescriptionAr { get; set; }
		public string? DescriptionEn { get; set; }
		public string AddressAr { get; set; }
		public string AddressEn { get; set; }
		public string? Logo { get; set; }
		public string? CoverImage { get; set; }
		public string? PostalCode { get; set; } // الرمز البريدي
		public string? MainPhone { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; } = true; // حالة المدرسة
		public DateTime? DeactivatedDate { get; set; } // تاريخ إلغاء التفعيل، إذا تم إلغاء التفعيل
		public GenderType GenderType { get; set; } //  فئة الطلاب ,بنين, بنات بنين
		public SchoolType SchoolType { get; set; } // نوع المدرسة (خاصة، حكومية، دولية...)
		public CurriculumType CurriculumType { get; set; } // نوع المنهج
		public SchoolLevel SchoolLevel { get; set; } // مستوى المراحل الدراسية



		//Relation
		public Guid CityId { get; set; } // معرف المدينة
		public Guid RegionId { get; set; } // معرف المنطقة

		public City City { get; set; } = null!;
		public Region Region { get; set; } = null!;

		public ICollection<SchoolNews> SchoolNews { get; set; } = [];
		public ICollection<SchoolPhoto> SchoolPhotos { get; set; } = [];
		public ICollection<SchoolPhone> SchoolPhones { get; set; } = [];
		public ICollection<SchoolRating> SchoolRatings { get; set; } = [];
		public ICollection<Stage> Stages { get; set; } = [];
		public ICollection<Teacher> Teachers { get; set; } = [];
		public ICollection<Subject> Subjects { get; set; } = [];
	}
}