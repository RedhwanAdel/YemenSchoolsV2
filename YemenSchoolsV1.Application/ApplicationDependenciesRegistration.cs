using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YemenSchoolsV1.Application.Behaviors;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Parents;
using YemenSchoolsV1.Application.Features.Schools;
using YemenSchoolsV1.Application.Services.Implementations;

namespace YemenSchoolsV1.Application
{
	public static class ApplicationDependenciesRegistration
	{
		public static IServiceCollection AddConfigureApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			// 
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register Services
            services.AddScoped<ITokenService, TokenService>();

			return services;
		}
	}
}
