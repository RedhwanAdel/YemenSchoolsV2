namespace YemenSchoolsV1.Domain.Entities
{
	public class Grade
	{
		public Guid Id { get; set; } // معرف الصف الدراسي
		public Guid TermId { get; set; } // معرف العام الدراسي المرتبط
		public string Name { get; set; } // اسم الصف الدراسي
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاريخ الإنشاء
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // تاريخ آخر تعديل
		public bool IsActive { get; set; } = true; // حالة التفعيل (فعال/معطل)

		// Navigation Property
		public Term Term { get; set; } = null!;
		public ICollection<Section> Sections { get; set; } = new List<Section>();
		public ICollection<SubjectGrade> SubjectGrades { get; set; } = new List<SubjectGrade>();
	}
}
