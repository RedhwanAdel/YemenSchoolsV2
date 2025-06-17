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
		public DbSet<SchoolRating> SchoolRatings { get; set; }
		public DbSet<SchoolPhoto> SchoolPhotos { get; set; }
		public DbSet<SchoolPhone> schoolPhones { get; set; }
		public DbSet<NewsPhoto> NewsPhotos { get; set; }
		public DbSet<AcademicYear> AcademicYears { get; set; }
		public DbSet<Term> Terms { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<SubjectGrade> SubjectGrades { get; set; }
		public DbSet<AssignedSubject> AssignedSubjects { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Parent> Parents { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<School>()
		   .HasMany(s => s.Stages)
		   .WithOne(s => s.School)
		   .HasForeignKey(s => s.SchoolId)
		   .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

			modelBuilder.Entity<Stage>()
			  .HasMany(t => t.AcademicYears)
			  .WithOne(s => s.Stage)
			  .HasForeignKey(s => s.StageId)
			  .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete


			modelBuilder.Entity<AcademicYear>()
				.HasMany(ay => ay.Terms)
				.WithOne(t => t.AcademicYear)
				.HasForeignKey(t => t.AcademicYearId)
				.OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete


			modelBuilder.Entity<Term>()
				.HasMany(s => s.Grades)
				.WithOne(g => g.Term)
				.HasForeignKey(g => g.TermId)
				.OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

			modelBuilder.Entity<Grade>()
				.HasMany(g => g.Sections)
				.WithOne(gr => gr.Grade)
				.HasForeignKey(gr => gr.GradeId)
				.OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete


			modelBuilder.ApplyConfiguration(new SchoolConfiguration());
			modelBuilder.ApplyConfiguration(new RegionConfiguration());
			modelBuilder.ApplyConfiguration(new CityConfiguration());
			modelBuilder.ApplyConfiguration(new SubjectConfiguration());
			modelBuilder.ApplyConfiguration(new TeacherConfiguration());
			modelBuilder.ApplyConfiguration(new GradeConfiguration());
			modelBuilder.ApplyConfiguration(new SectionConfiguration());
			modelBuilder.ApplyConfiguration(new SubjectGradeConfiguration());
			modelBuilder.ApplyConfiguration(new AssignedSubjectConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new StudentConfiguration());
			modelBuilder.ApplyConfiguration(new ParentConfiguration());
			modelBuilder.ApplyConfiguration(new ParentStudentConfiguration());

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

