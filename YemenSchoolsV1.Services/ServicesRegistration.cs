using Microsoft.Extensions.DependencyInjection;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Accounts;
using YemenSchoolsV1.Services.Implementations;


namespace FinalProject.Services
{
	public static class ServicesRegistration
	{
		public static IServiceCollection AddConfigureServices(this IServiceCollection services)
		{

			// تسجيل الخدمات Scoped
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



			return services;
		}
	}
}
