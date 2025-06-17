using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            // تعيين المفتاح الأساسي
            builder.HasKey(r => r.Id);

            // تعريف الحقول المطلوبة
            builder.Property(r => r.NameAr).IsRequired().HasMaxLength(100);
            builder.Property(r => r.NameEn).IsRequired().HasMaxLength(100);

         
            // إعدادات العلاقات
            builder.HasOne(r => r.City)
                   .WithMany(c => c.Regions)
                   .HasForeignKey(r => r.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Schools)
                   .WithOne(s => s.Region)
                   .HasForeignKey(s => s.RegionId);
        }
    }
}
