namespace YemenSchoolsV1.Domain.Entities
{
	public class Term
	{
		public Guid Id { get; set; } // معرف الفصل الدراسي
		public string Name { get; set; } // اسم الفصل الدراسي (مثل الفصل الأول)
		public Guid AcademicYearId { get; set; } // معرف العام الدراسي المرتبط
		public DateTime StartDate { get; set; } // تاريخ بداية الفصل الدراسي
		public DateTime EndDate { get; set; } // تاريخ نهاية الفصل الدراسي
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاريخ الإنشاء
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // تاريخ آخر تعديل
		public bool IsActive { get; set; } = true; // حالة التفعيل (فعال/معطل)

		// Navigation Property
		public AcademicYear AcademicYear { get; set; } = null!;
		public ICollection<Grade> Grades { get; set; } = [];
	}
}
