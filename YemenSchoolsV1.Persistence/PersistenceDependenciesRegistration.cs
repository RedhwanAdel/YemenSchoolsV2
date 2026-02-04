using Microsoft.Extensions.DependencyInjection;
using YemenSchoolsV1.Application.Contracts;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Persistence.Reports;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Persistence
{
    public static class PersistenceDependenciesRegistration
    {
        public static IServiceCollection AddConfigurePersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();

            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolNewsRepository, SchoolNewsRepository>();

            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<ITermRepository, TermRepository>();
            services.AddScoped<IStageRepository, StageRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStageGradeRepository, StageGradeRepository>();
            services.AddScoped<ISchoolGradeRepository, SchoolGradeRepository>();
            services.AddScoped<ISectionSubjectRepository, SectionSubjectRepository>();
            services.AddScoped<IParentRepository, ParentRepository>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IMarkRepository, MarkRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ISchoolReviewRepository, SchoolReviewRepository>();
            services.AddScoped<IDailyLogRepository, DailyLogRepository>();
            services.AddScoped<IStudentReportService, StudentReportService>();
            services.AddScoped<ISchoolReportService, SchoolReportService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();



            return services;
        }
    }
}
