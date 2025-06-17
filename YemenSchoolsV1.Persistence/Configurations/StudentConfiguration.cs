using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(s => s.Id);

			builder.HasOne(s => s.User)
				   .WithOne()
				   .HasForeignKey<Student>(s => s.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(s => s.Section)
				   .WithMany(sec => sec.Students)
				   .HasForeignKey(s => s.SectionId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(s => s.Parents)
				   .WithOne(ps => ps.Student)
				   .HasForeignKey(ps => ps.StudentId);

			builder.Property(s => s.RegisterNo).IsRequired().HasMaxLength(50);
			builder.Property(s => s.NameAr).IsRequired().HasMaxLength(100);
			builder.Property(s => s.NameEn).IsRequired().HasMaxLength(100);
		}
	}

}
