using FinalProject.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Persistence
{
	public static class PresistenceDependenciesRegistration
	{
		public static IServiceCollection AddConfigurePersistenceServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
			services.AddScoped<ICityRepositry, CityRepositry>();
			services.AddScoped<IRegionRepositry, RegionRepositry>();

			services.AddScoped<ISchoolRepositry, SchoolRepositry>();
			services.AddScoped<ISchoolNewsRepositry, SchoolNewsRepositry>();

			services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
			services.AddScoped<ITermRepositry, TermRepositry>();
			services.AddScoped<IStageRepositry, StageRepositry>();
			services.AddScoped<IGradeRepositry, GradeRepositry>();
			services.AddScoped<ISectionRepositry, SectionRepositry>();
			services.AddScoped<ISubjectRepositry, SubjectRepositry>();
			services.AddScoped<ITeacherRepositry, TeacherRepositry>();
			services.AddScoped<ISubjectGradeRepositry, SubjectGradeRepository>();
			services.AddScoped<IAssignedSubjectRepository, AssignedSubjectRepository>();
			services.AddScoped<IStudentRepository, StudentRepository>();

			return services;
		}
	}
}