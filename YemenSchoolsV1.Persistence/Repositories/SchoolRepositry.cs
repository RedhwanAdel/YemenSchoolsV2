using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SchoolRepositry : GenericRepositoryAsync<School>, ISchoolRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public SchoolRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateSchoolPhonesRangAsync(List<SchoolPhone> schoolPhones)
        {
            await dbContext.SchoolPhones.AddRangeAsync(schoolPhones);
            await dbContext.SaveChangesAsync();
        }

        public async Task<School?> GetSchoolDetailsInculdeAsync(Guid schoolId)
        {
            return await dbContext.Schools.Include(c => c.City).Include(r => r.Region).Include(ph => ph.SchoolPhones).FirstOrDefaultAsync(s => s.Id == schoolId);

        }


        public async Task<SchoolForUpdate?> GetSchoolByIdForUpdateAsync(Guid schoolId)
        {
            return await dbContext.Schools
       .Include(s => s.City)
       .Include(s => s.Region)
       .Include(s => s.SchoolPhones)
       .Where(s => s.Id == schoolId)
       .Select(s => new SchoolForUpdate
       {
           Id = s.Id,
           NameAr = s.NameAr,
           NameEn = s.NameEn,
           AddressAr = s.AddressAr,
           AddressEn = s.AddressEn,
           PostalCode = s.PostalCode,
           MainPhone = s.MainPhone,
           Email = s.Email,
           SchoolType = (int)s.SchoolType,
           GenderType = (int)s.GenderType,
           CurriculumType = (int)s.CurriculumType,
           SchoolLevel = (int)s.SchoolLevel,
           CityId = s.CityId,
           CityName = s.City.NameEn,
           RegionId = s.RegionId,
           RegionName = s.Region.NameEn,
           PhoneNumberList = s.SchoolPhones.Select(p => p.PhoneNumber).ToList()
       })
       .FirstOrDefaultAsync();

        }
        public IQueryable<School> GetSchoolsWithCityAndRegionQueryable()
        {
            return GetTableNoTracking()
         .Include(s => s.City)
         .Include(s => s.Region);

        }


        public async Task AssignSubjectsToSchoolGradeAsync(Guid schoolGradeId, List<Guid> subjectIds)
        {
            if (subjectIds == null)
                return;

            var existingAssignments = await dbContext.GradeSubject
                                .AsNoTracking()
                                .Where(gs => gs.SchoolGradeId == schoolGradeId)
                                .ToListAsync();

            dbContext.GradeSubject.RemoveRange(existingAssignments.Where(ea => !subjectIds.Contains(ea.SubjectId)));

            var existingSubjectIds = existingAssignments.Select(ea => ea.SubjectId).ToList();
            var newSubjectIds = subjectIds.Except(existingSubjectIds).ToList();

            foreach (var newSubjectId in newSubjectIds)
            {
                dbContext.GradeSubject.Add(new GradeSubject
                {
                    SchoolGradeId = schoolGradeId,
                    SubjectId = newSubjectId
                });
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<SubjectDto>> GetSubjectsForSchoolGradeAsync(Guid schoolGradeId)
        {
            return await dbContext.GradeSubject
                                 .Where(gs => gs.SchoolGradeId == schoolGradeId)
                                 .Select(gs => new SubjectDto { Id = gs.Subject.Id, Name = gs.Subject.Name, gradeSubjectId = gs.Id })
                                 .ToListAsync();
        }



        public async Task<SchoolReportDto?> GetSchoolReportAsync(Guid schoolId)
        {
            var school = await dbContext.Schools
                .Include(s => s.City)
                .Include(s => s.Region)
                .Include(s => s.SchoolPhones)
                .Include(s => s.Teachers)
                .Include(s => s.SchoolGrades)
                    .ThenInclude(sg => sg.GradeSubjects)
                .Include(s => s.SchoolGrades)
                    .ThenInclude(sg => sg.Sections)
                .Include(s => s.AcademicYears)
                    .ThenInclude(ay => ay.Sections)
                .Include(s => s.SchoolNews)
                .Include(s => s.SchoolPhotos)
                .Include(s => s.SchoolRatings)
                .FirstOrDefaultAsync(s => s.Id == schoolId);

            if (school == null)
                return null;

            // Count unique grades, subjects, sections, students, etc.
            var grades = school.SchoolGrades;
            var subjectsCount = grades.SelectMany(g => g.GradeSubjects).Select(gs => gs.SubjectId).Distinct().Count();
            var sectionsCount = grades.SelectMany(g => g.Sections).Count();
            var studentsCount = grades.SelectMany(g => g.Sections).SelectMany(sec => sec.SectionSubjects);
            var academicYearsCount = school.AcademicYears.Count;
            var newsCount = school.SchoolNews.Count;
            var photosCount = school.SchoolPhotos.Count;
            var ratingsCount = school.SchoolRatings.Count;
            var parentsCount = 0; // If you have a way to count parents, add logic here

            return new SchoolReportDto
            {
                SchoolId = school.Id,
                NameAr = school.NameAr,
                NameEn = school.NameEn,
                DescriptionAr = school.DescriptionAr,
                AddressAr = school.AddressAr,
                PostalCode = school.PostalCode,
                MainPhone = school.MainPhone,
                Email = school.Email,
                SchoolType = (int)school.SchoolType,
                SchoolLevel = (int)school.SchoolLevel,
                GenderType = (int)school.GenderType,
                CurriculumType = (int)school.CurriculumType,
                CityId = school.CityId,
                CityNameAr = school.City?.NameAr,
                RegionId = school.RegionId,
                RegionNameAr = school.Region?.NameAr,
                PhoneNumbers = school.SchoolPhones.Select(p => p.PhoneNumber).ToList(),
                TeachersCount = school.Teachers.Count,
                GradesCount = grades.Count,
                SubjectsCount = subjectsCount,
                SectionsCount = sectionsCount,
                AcademicYearsCount = academicYearsCount,
                NewsCount = newsCount,
                PhotosCount = photosCount,
                ParentsCount = parentsCount,
                RatingsCount = ratingsCount
            };
        }

    }
}
