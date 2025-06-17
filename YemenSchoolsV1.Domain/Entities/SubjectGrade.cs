namespace YemenSchoolsV1.Domain.Entities
{
	public class SubjectGrade
	{

		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; } = null!;

		public Guid GradeId { get; set; }
		public Grade Grade { get; set; } = null!;

		public decimal MinPassMark { get; set; }
		public decimal MaxMark { get; set; }
	}
}