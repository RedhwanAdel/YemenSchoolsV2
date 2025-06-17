using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Accounts.Login
{
	public class LoginCommand : IRequest<Response<AuthResultDto>>
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
