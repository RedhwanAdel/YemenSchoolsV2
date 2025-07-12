namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated
{
	public class GetSchoolPagenatedListResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Logo { get; set; }
		public string SchoolType { get; set; }
		public string GenderType { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string? CoverImage { get; set; }
		public string? MainPhone { get; set; }
		public string SchoolLevel { get; set; }

		//  public double Rating { get; set; } 


	}
}
