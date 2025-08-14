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
        public DbSet<City> Citys { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolNews> SchoolNews { get; set; }
        public DbSet<SchoolGrade> SchoolGrade { get; set; }
        public DbSet<SchoolRating> SchoolRatings { get; set; }
        public DbSet<SchoolPhoto> SchoolPhotos { get; set; }
        public DbSet<SchoolPhone> SchoolPhones { get; set; }
        public DbSet<NewsPhoto> NewsPhotos { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeSubject> GradeSubject { get; set; }
        public DbSet<StageGrade> StageGrade { get; set; }
        public DbSet<SectionSubject> SectionSubject { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<ParentStudent> ParentStudents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceDetail> AttendanceDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //stage
            modelBuilder.Entity<Stage>()
                .HasMany(s => s.StageGrades)
                .WithOne(sg => sg.Stage)
                .HasForeignKey(sg => sg.StageId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Stage>().HasData(
                   new Grade { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Name = "المرحلة الإبتدائية" },
                   new Grade { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), Name = "المرحلة الإعدادية" },
                   new Grade { Id = Guid.Parse("22222222-2222-2222-2222-222222222783"), Name = "الروضة" },
                   new Grade { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), Name = "المرحلة الثانوية" }
);

            //Stage grade 



            //School grade 
            modelBuilder.Entity<SchoolGrade>()
                .HasMany(sg => sg.GradeSubjects)
                .WithOne(gs => gs.SchoolGrade)
                .HasForeignKey(gs => gs.SchoolGradeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SchoolGrade>()
                .HasMany(sg => sg.Sections)
                .WithOne(cs => cs.SchoolGrade)
                .HasForeignKey(cs => cs.SchoolGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Grade subject 
            modelBuilder.Entity<GradeSubject>()
                .HasMany(gs => gs.SectionSubjects)
                .WithOne(ss => ss.GradeSubject)
                .HasForeignKey(ss => ss.GradeSubjectId)
                .OnDelete(DeleteBehavior.Restrict);



            //acadmicyear
            modelBuilder.Entity<AcademicYear>()
                .HasMany(ay => ay.Sections)
                .WithOne(cs => cs.AcademicYear)
                .HasForeignKey(cs => cs.AcademicYearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
              .HasMany(ay => ay.Terms)
              .WithOne(s => s.AcademicYear)
              .HasForeignKey(s => s.AcademicYearId)
              .OnDelete(DeleteBehavior.Restrict);

            //term
            modelBuilder.Entity<Term>()
                .HasMany(s => s.SectionSubjects)
                .WithOne(ss => ss.Term)
                .HasForeignKey(ss => ss.TermId)
                .OnDelete(DeleteBehavior.Restrict);



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

