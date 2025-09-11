using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Persistence.Data
{
    public class DataSeeder
    {

        public static async Task SeedTeacherAsync(YemenShoolsDbContext _context, IPasswordHasher<AppUser> hasher)
        {
            var faker = new Bogus.Faker("ar");
            var random = new Random();


            if (!_context.Teachers.Any())
            {
                var specializations = new[] { "رياضيات", "لغة عربية", "علوم", "إنجليزي", "قرآن", "حاسوب" };
                Guid schoolsId = Guid.Parse("4cbe0fe1-5730-49c9-8e8e-1800ecea6345");

                for (int i = 0 ; i < 10 ; i++)
                {
                    var email = $"teacher{i}@test.com";
                    var user = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        NormalizedUserName = email.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserType = "Teacher"
                    };
                    user.PasswordHash = hasher.HashPassword(user, "Pa$$w0rd");
                    await _context.Users.AddAsync(user);

                    var teacher = new Teacher
                    {
                        Id = Guid.NewGuid(),
                        NameAr = faker.Name.FullName(),
                        NameEn = faker.Name.FullName(),
                        Email = email,
                        PhoneNumber = "777777777",
                        Address = faker.Address.FullAddress(),
                        Gender = Gender.Male,
                        SchoolId = schoolsId, // استخدم الـ GUID الصحيح
                        UserId = user.Id,
                        User = user, // ربط الكيان
                        Specialization = specializations[random.Next(specializations.Length)]
                    };

                    await _context.Teachers.AddAsync(teacher);
                    user.EntityId = teacher.Id;
                }

                await _context.SaveChangesAsync();

            }
        }
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


        //public static async Task SeedFullAsync(
        //YemenShoolsDbContext context,
        //UserManager<AppUser> userManager,
        //IPasswordHasher<AppUser> hasher)
        //{
        //    var faker = new Bogus.Faker("ar");
        //    var random = new Random();

        //    // 1. AcademicYear
        //    if (!context.AcademicYears.Any())
        //    {
        //        var school = context.Schools.First(); // أول مدرسة من JSON
        //        context.AcademicYears.Add(new AcademicYear
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "2024/2025",
        //            StartDate = new DateTime(2024, 9, 1),
        //            EndDate = new DateTime(2025, 6, 30),
        //            IsCurrentYear = true,
        //            SchoolId = school.Id
        //        });
        //        await context.SaveChangesAsync();
        //    }
        //    var academicYear = context.AcademicYears.First();

        //    // 2. StageGrade
        //    if (!context.StageGrade.Any())
        //    {
        //        var stages = context.Stages.ToList();
        //        var grades = context.Grades.ToList();

        //        foreach (var stage in stages)
        //        {
        //            foreach (var grade in grades)
        //            {
        //                context.StageGrade.Add(new StageGrade
        //                {
        //                    Id = Guid.NewGuid(),
        //                    StageId = stage.Id,
        //                    GradeId = grade.Id
        //                });
        //            }
        //        }
        //        await context.SaveChangesAsync();
        //    }

        //    // 3. SchoolGrade
        //    if (!context.SchoolGrade.Any())
        //    {
        //        var schools = context.Schools.ToList();
        //        var stageGrades = context.StageGrade.ToList();

        //        foreach (var school in schools)
        //        {
        //            foreach (var sg in stageGrades.Take(6))
        //            {
        //                context.SchoolGrade.Add(new SchoolGrade
        //                {
        //                    Id = Guid.NewGuid(),
        //                    SchoolId = school.Id,
        //                    StageGradeId = sg.Id
        //                });
        //            }
        //        }
        //        await context.SaveChangesAsync();
        //    }

        //    // 4. Teachers + Users
        //    if (!context.Teachers.Any())
        //    {
        //        var schools = context.Schools.Take(3).ToList();
        //        var specializations = new[] { "رياضيات", "لغة عربية", "علوم", "إنجليزي", "قرآن", "حاسوب" };

        //        foreach (var school in schools)
        //        {
        //            for (int i = 0 ; i < 10 ; i++)
        //            {
        //                var email = faker.Internet.Email();
        //                var user = new AppUser
        //                {
        //                    UserName = email,
        //                    Email = email,
        //                    EmailConfirmed = true,
        //                    SecurityStamp = Guid.NewGuid().ToString(),
        //                    UserType = "Teacher"
        //                };
        //                user.PasswordHash = hasher.HashPassword(user, "Pa$$w0rd");
        //                await context.Users.AddAsync(user);

        //                var teacher = new Teacher
        //                {
        //                    Id = Guid.NewGuid(),
        //                    NameAr = faker.Name.FullName(),
        //                    NameEn = faker.Name.FullName(),
        //                    Email = email,
        //                    PhoneNumber = faker.Phone.PhoneNumber(),
        //                    Address = faker.Address.FullAddress(),
        //                    Gender = random.Next(0, 2) == 0 ? Gender.Male : Gender.Female,
        //                    SchoolId = school.Id,
        //                    UserId = user.Id,
        //                    Specialization = specializations[random.Next(specializations.Length)]
        //                };
        //                await context.Teachers.AddAsync(teacher);
        //                user.EntityId = teacher.Id;
        //            }
        //        }
        //        await context.SaveChangesAsync();
        //    }
        //    var teachers = context.Teachers.ToList();

        //    // 5. Sections + Students + Parents
        //    if (!context.Sections.Any())
        //    {
        //        var schoolGrades = context.SchoolGrade.ToList();
        //        foreach (var sg in schoolGrades.Take(5))
        //        {
        //            for (int j = 1 ; j <= 2 ; j++)
        //            {
        //                var section = new Section
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Name = $"شعبة {j}",
        //                    SchoolGradeId = sg.Id,
        //                    AcademicYearId = academicYear.Id,
        //                    ClassTeacherId = teachers[random.Next(teachers.Count)].Id
        //                };
        //                await context.Sections.AddAsync(section);

        //                // طلاب + أولياء أمور
        //                for (int i = 0 ; i < 20 ; i++)
        //                {
        //                    var studentEmail = faker.Internet.Email();
        //                    var studentUser = new AppUser
        //                    {
        //                        UserName = studentEmail,
        //                        Email = studentEmail,
        //                        EmailConfirmed = true,
        //                        SecurityStamp = Guid.NewGuid().ToString(),
        //                        UserType = "Student"
        //                    };
        //                    studentUser.PasswordHash = hasher.HashPassword(studentUser, "Pa$$w0rd");
        //                    await context.Users.AddAsync(studentUser);

        //                    var student = new Student
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        NameAr = faker.Name.FullName(),
        //                        NameEn = faker.Name.FullName(),
        //                        RegisterNo = faker.Random.AlphaNumeric(10).ToUpper(),
        //                        Address = faker.Address.FullAddress(),
        //                        Email = studentEmail,
        //                        Gender = random.Next(0, 2) == 0 ? Gender.Male : Gender.Female,
        //                        CurrentSectionId = section.Id,
        //                        CurrentAcademicYearId = academicYear.Id,
        //                        SchoolId = sg.SchoolId,
        //                        UserId = studentUser.Id,
        //                        Nationality = "يمني"
        //                    };
        //                    await context.Students.AddAsync(student);
        //                    studentUser.EntityId = student.Id;

        //                    var parentEmail = faker.Internet.Email();
        //                    var parentUser = new AppUser
        //                    {
        //                        UserName = parentEmail,
        //                        Email = parentEmail,
        //                        EmailConfirmed = true,
        //                        SecurityStamp = Guid.NewGuid().ToString(),
        //                        UserType = "Parent"
        //                    };
        //                    parentUser.PasswordHash = hasher.HashPassword(parentUser, "Pa$$w0rd");
        //                    await context.Users.AddAsync(parentUser);
        //                    var nationalId = faker.Random.Number(10000000, 999999999).ToString();

        //                    var parent = new Parent
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        NameAr = faker.Name.FullName(),
        //                        NameEn = faker.Name.FullName(),
        //                        Address = faker.Address.FullAddress(),
        //                        NationalId = nationalId,
        //                        Email = parentEmail,
        //                        PhoneNumber = faker.Phone.PhoneNumber(),
        //                        UserId = parentUser.Id
        //                    };
        //                    await context.Parents.AddAsync(parent);
        //                    parentUser.EntityId = parent.Id;

        //                    await context.ParentStudents.AddAsync(new ParentStudent
        //                    {
        //                        ParentId = parent.Id,
        //                        StudentId = student.Id,
        //                        RelationType = "أب"
        //                    });
        //                }
        //            }
        //        }
        //        await context.SaveChangesAsync();
        //    }

        //    // 6. Attendance
        //    if (!context.Attendances.Any())
        //    {
        //        var sections = context.Sections.ToList();
        //        foreach (var section in sections)
        //        {
        //            for (int d = 0 ; d < 5 ; d++)
        //            {
        //                var attendance = new Attendance
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Date = DateTime.Now.AddDays(-d),
        //                    SectionId = section.Id,
        //                    AcademicYearId = academicYear.Id,
        //                    ClassTeacherId = section.ClassTeacherId ?? Guid.NewGuid(),
        //                    IsDayOff = false
        //                };
        //                await context.Attendances.AddAsync(attendance);

        //                var students = context.Students.Where(s => s.CurrentSectionId == section.Id).ToList();
        //                foreach (var st in students)
        //                {
        //                    await context.AttendanceDetails.AddAsync(new AttendanceDetail
        //                    {
        //                        Id = Guid.NewGuid(),
        //                        AttendanceId = attendance.Id,
        //                        StudentId = st.Id,
        //                        Status = random.Next(0, 100) > 20 ? AttendanceStatus.Present : AttendanceStatus.AbsentWithoutExcuse
        //                    });
        //                }
        //            }
        //        }
        //        await context.SaveChangesAsync();
        //    }
        //}


    }

    public class SeedModel
    {
        public List<City> Cities { get; set; }
        public List<Region> Regions { get; set; }
        public List<School> Schools { get; set; }

    }

}

