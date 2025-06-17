using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Accounts.Register
{
	public class RegisterCommand : IRequest<Response<AuthResultDto>>
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required string FullName { get; set; }
	}
}
