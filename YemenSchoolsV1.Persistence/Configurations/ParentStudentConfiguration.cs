using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
	public class ParentStudentConfiguration : IEntityTypeConfiguration<ParentStudent>
	{
		public void Configure(EntityTypeBuilder<ParentStudent> builder)
		{
			builder.HasKey(ps => new { ps.ParentId, ps.StudentId });

			builder.HasOne(ps => ps.Parent)
				   .WithMany(p => p.Students)
				   .HasForeignKey(ps => ps.ParentId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(ps => ps.Student)
				   .WithMany(s => s.Parents)
				   .HasForeignKey(ps => ps.StudentId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.Property(ps => ps.RelationType)
				   .IsRequired()
				   .HasMaxLength(50);
		}
	}

}
