using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Extensions;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Accounts.Register
{
	public class RegisterCommandHandler : ResponseHandler, IRequestHandler<RegisterCommand, Response<string>>
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


		public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
				return BadRequest<string>(SharedResourcesKeys.EmailExist);
			var user = mapper.Map<AppUser>(request);
			user.UserName = request.Email;

			var result = await userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{



				return ValidationError<string>(result.ToErrorDictionary());
			}
			return Success(SharedResourcesKeys.Success);


		}
	}
}
