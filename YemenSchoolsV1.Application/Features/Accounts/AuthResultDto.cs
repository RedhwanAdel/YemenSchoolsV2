namespace YemenSchoolsV1.Application.Features.Accounts
{
	public class AuthResultDto
	{
		public Guid UserId { get; set; }
		public required string Email { get; set; }
		public required string Token { get; set; }
	}
}
