using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Configurations;

namespace YemenSchoolsV1.Persistence.Data
{
    public class YemenShoolsDbContext : IdentityDbContext<AppUser,
        AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public YemenShoolsDbContext(DbContextOptions<YemenShoolsDbContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolNews> SchoolNews { get; set; }
        public DbSet<SchoolGrade> SchoolGrades { get; set; }
        public DbSet<SchoolReview> SchoolReviews { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }
        public DbSet<SchoolPhoto> SchoolPhotos { get; set; }
        public DbSet<SchoolPhone> SchoolPhones { get; set; }
        public DbSet<NewsPhoto> NewsPhotos { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeSubject> GradeSubjects { get; set; }
        public DbSet<StageGrade> StageGrades { get; set; }
        public DbSet<SectionSubject> SectionSubjects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<ParentStudent> ParentStudents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceDetail> AttendanceDetails { get; set; }
        public DbSet<Mark> Marks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StageGradeConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new ParentConfiguration());
            modelBuilder.ApplyConfiguration(new ParentStudentConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new DailyLogConfiguration());
            modelBuilder.ApplyConfiguration(new StageConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicYearConfiguration());
            modelBuilder.ApplyConfiguration(new TermConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolGradeConfiguration());
            modelBuilder.ApplyConfiguration(new GradeSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolReviewConfiguration());

            base.OnModelCreating(modelBuilder);
        }



        //public static async Task SeedAsync(YemenShoolsDbContext context, ILoggerFactory loggerFactory)
        //{
        //	var logger = loggerFactory.CreateLogger<DataSeeder>();
        //	try
        //	{
        //		var seeder = new DataSeeder(context, logger);
        //		await seeder.SeedDataAsync();
        //	}
        //	catch (Exception ex)
        //	{
        //		logger.LogError(ex, "An error occurred during seeding");
        //	}
        //}

    }
}

