namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
	public class GetYearListResponse
	{
		public Guid Id { get; set; }

		public string Name { get; set; }
		public Guid StageId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
