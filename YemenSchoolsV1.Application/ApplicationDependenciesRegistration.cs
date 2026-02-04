using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YemenSchoolsV1.Application.Behaviors;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.AttendanceStudents;
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
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISchoolNewsService, SchoolNewsService>();
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<ITermService, TermService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<ISchoolReviewService, SchoolReviewService>();

			return services;
		}
	}
}
