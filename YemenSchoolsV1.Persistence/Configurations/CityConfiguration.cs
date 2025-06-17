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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // تعيين المفتاح الأساسي
            builder.HasKey(c => c.Id);

            // تعريف الحقول المطلوبة
            builder.Property(c => c.NameAr).IsRequired().HasMaxLength(100);
            builder.Property(c => c.NameEn).IsRequired().HasMaxLength(100);

          
            // إعدادات العلاقات
            builder.HasMany(c => c.Regions)
                   .WithOne(r => r.City)
                   .HasForeignKey(r => r.CityId);

            builder.HasMany(c => c.Schools)
                   .WithOne(s => s.City)
                   .HasForeignKey(s => s.CityId);
        }
    }
}
