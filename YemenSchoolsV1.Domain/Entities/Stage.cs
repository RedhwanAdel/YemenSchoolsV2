namespace YemenSchoolsV1.Domain.Entities
{
	public class Stage
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid SchoolId { get; set; }
		public School School { get; set; } = null!;
		public ICollection<AcademicYear> AcademicYears { get; set; } = [];
	}
}
