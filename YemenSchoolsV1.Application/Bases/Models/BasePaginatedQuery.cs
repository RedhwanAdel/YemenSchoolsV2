namespace YemenSchoolsV1.Application.Bases.Models
{
	public class BasePaginatedQuery
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string? Search { get; set; }
		public string SortDirection { get; set; } = "asc";
	}

}
