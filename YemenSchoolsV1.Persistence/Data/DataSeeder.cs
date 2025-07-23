using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Persistence.Data
{
    public class DataSeeder
    {

        public static async Task SeedAsync(YemenShoolsDbContext _context)
        {

            if (await _context.Citys.AnyAsync()) return;
            var path = Path.Combine(AppContext.BaseDirectory, "Data", "schools_data.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Could not find seed data file at path: {path}");
            }

            var jsonData = await File.ReadAllTextAsync(path);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var data = JsonSerializer.Deserialize<SeedModel>(jsonData, options);

            // إضافة البيانات إلى قاعدة البيانات
            await _context.Citys.AddRangeAsync(data.Cities);
            await _context.Regions.AddRangeAsync(data.Regions);
            await _context.Schools.AddRangeAsync(data.Schools);

            await _context.SaveChangesAsync();
        }






    }

    public class SeedModel
    {
        public List<City> Cities { get; set; }
        public List<Region> Regions { get; set; }
        public List<School> Schools { get; set; }

    }

}

