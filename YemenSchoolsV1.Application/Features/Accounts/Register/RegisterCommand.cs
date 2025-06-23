using FinalProject.Application.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace YemenSchoolsV1.Application.Features.Accounts.Register
{
	public class RegisterCommand : IRequest<Response<string>>
	{
		[Required]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]

		public string FirstName { get; set; } = string.Empty;
		[Required]

		public string LastName { get; set; } = string.Empty;
	}
}
