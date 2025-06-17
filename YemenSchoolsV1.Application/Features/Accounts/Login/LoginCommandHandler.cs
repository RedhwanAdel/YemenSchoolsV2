using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Accounts.Login
{
	public class LoginCommandHandler : ResponseHandler, IRequestHandler<LoginCommand, Response<AuthResultDto>>
	{
		#region faild

		private readonly UserManager<AppUser> userManager;
		private readonly ITokenService tokenService;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public LoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.userManager = userManager;
			this.tokenService = tokenService;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;


		}



		#endregion

		public async Task<Response<AuthResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByEmailAsync(request.Email);
			if (user == null) return BadRequest<AuthResultDto>("Invalid credentials");
			var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
			if (!isPasswordValid) return BadRequest<AuthResultDto>("Invalid credentials");

			var token = await tokenService.CreateToken(user);

			if (user.Email == null) return BadRequest<AuthResultDto>("Email was not provided or saved correctly");
			var authResultDto = new AuthResultDto
			{
				Email = user.Email,
				Token = token,
				UserId = user.Id
			};
			return Created(authResultDto);


		}

	}
}
