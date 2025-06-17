using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Accounts.Register
{
	public class RegisterCommandHandler : ResponseHandler, IRequestHandler<RegisterCommand, Response<AuthResultDto>>
	{

		#region faild

		private readonly UserManager<AppUser> userManager;
		private readonly ITokenService tokenService;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.userManager = userManager;
			this.tokenService = tokenService;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;


		}

		#endregion


		public async Task<Response<AuthResultDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
				return BadRequest<AuthResultDto>("email is exist");
			var user = mapper.Map<AppUser>(request);

			var result = await userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
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
			return BadRequest<AuthResultDto>(string.Join("; ", result.Errors.Select(e => e.Description)));


		}
	}
}
