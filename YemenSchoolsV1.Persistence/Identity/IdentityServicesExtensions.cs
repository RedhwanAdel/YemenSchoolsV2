using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Identity
{
	public static class IdentityServicesExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddIdentityCore<AppUser>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
			})
				.AddRoles<AppRole>()
				.AddRoleManager<RoleManager<AppRole>>()
				.AddEntityFrameworkStores<YemenShoolsDbContext>();



			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
			{
				var tokenkey = config["TokenKey"] ?? throw new Exception("token key not found");
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
					ValidateIssuer = false,
					ValidateAudience = false,
				};
				//option.Events = new JwtBearerEvents
				//{
				//	OnMessageReceived = context =>
				//	{
				//		var accessToken = context.Request.Query["access_token"];

				//		var path = context.HttpContext.Request.Path;
				//		if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
				//		{
				//			context.Token = accessToken;
				//		}

				//		return Task.CompletedTask;
				//	}
				//};
			});

			return services;
		}
	}
}
