namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
	public class GetStagesListResponse
	{
		public Guid Id { get; set; }

		public required string Name { get; set; }
		public Guid SchoolId { get; set; }
	}
}
