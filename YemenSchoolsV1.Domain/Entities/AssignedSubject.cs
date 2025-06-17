namespace YemenSchoolsV1.Domain.Entities
{
	public class AssignedSubject
	{

		public Guid TeacherId { get; set; }
		public Teacher Teacher { get; set; } = null!;

		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; } = null!;

		public Guid SectionId { get; set; }
		public Section Section { get; set; } = null!;
		public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

	}
}
