namespace YemenSchoolsV1.Domain.Entities
{
	public class Section
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid GradeId { get; set; }

		public int? RoomNumber { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public bool IsActive { get; set; } = true; // حالة التفعيل (فعال/معطل)

		// Navigation Property
		public Grade Grade { get; set; } = null!; // الصف الدراسي المرتبط
		public ICollection<AssignedSubject> AssignedSubjects { get; set; } = [];
		public ICollection<Student> Students { get; set; } = [];

	}
}
