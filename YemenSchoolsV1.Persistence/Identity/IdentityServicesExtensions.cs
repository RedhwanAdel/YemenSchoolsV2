using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Identity
{
	public static class IdentityServicesExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddAuthorization();

			services.AddIdentityApiEndpoints<AppUser>()
				.AddRoles<AppRole>()
				.AddRoleManager<RoleManager<AppRole>>()
				.AddEntityFrameworkStores<YemenShoolsDbContext>();



			//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
			//{
			//	var tokenkey = config["TokenKey"] ?? throw new Exception("token key not found");
			//	option.TokenValidationParameters = new TokenValidationParameters
			//	{
			//		ValidateIssuerSigningKey = true,
			//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
			//		ValidateIssuer = false,
			//		ValidateAudience = false,
			//	};

			//});

			return services;
		}
	}
}
