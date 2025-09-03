using Microsoft.EntityFrameworkCore;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class ParentRepositry : GenericRepositoryAsync<Parent>, IParentRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public ParentRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Student>> GetStudentsByParentIdAsync(Guid parentId)
        {
            return await dbContext.ParentStudents
                .Where(ps => ps.ParentId == parentId)

                    .Include(ps => ps.Student)
                    .ThenInclude(s => s.School)
                    .Include(ps => ps.Student)
                        .ThenInclude(s => s.CurrentSection)
                            .ThenInclude(sec => sec.SchoolGrade)
                                .ThenInclude(grade => grade.StageGrade)
                                    .ThenInclude(stage => stage.Grade)

                                    .Select(ps => ps.Student)
                                    .ToListAsync();
        }
        public async Task<Parent?> GetParentByNationalIdAsync(string nationalId)
        {
            return await dbContext.Parents
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.NationalId == nationalId);
        }
        public async Task<bool> ParentExistsByNationalIdAsync(string nationalId) =>
        await dbContext.Parents.AnyAsync(p => p.NationalId == nationalId);

        public async Task<Parent?> GetParentByUserIdAsync(Guid userId) =>
            await dbContext.Parents.FirstOrDefaultAsync(p => p.UserId == userId);
        public async Task<Parent?> GetParentByIdWithStudentsAsync(Guid parentId) =>
       await dbContext.Parents
                     .Include(p => p.Students)
                         .ThenInclude(ps => ps.Student)
                     .FirstOrDefaultAsync(p => p.Id == parentId);
        public async Task<IEnumerable<Parent>> GetAllParentsAsync() =>
       await dbContext.Parents.ToListAsync();



        public async Task DeactivateParentAsync(Guid parentId)
        {
            var parent = await dbContext.Parents.FindAsync(parentId);
            if (parent != null)
            {
                parent.IsActive = false;
                // Also find the user and deactivate
                var user = await dbContext.Users.FindAsync(parent.UserId);
                if (user != null)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                }
                await dbContext.SaveChangesAsync();
            }
        }

        //public async Task<(bool Succeeded, string Message)> DeleteParentAndRelatedDataAsync(Parent parent)
        //{
        //    using var transaction = await dbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        // Delete all ParentStudent relationships for this parent
        //        var parentStudents = dbContext.ParentStudents.Where(ps => ps.ParentId == parent.Id);
        //        dbContext.ParentStudents.RemoveRange(parentStudents);

        //        // Get the user before deleting the parent
        //        var user = await dbContext.Users.FindAsync(parent.UserId);

        //        // Delete the parent entity
        //        dbContext.Parents.Remove(parent);

        //        await dbContext.SaveChangesAsync();

        //        // Delete the user account
        //        if (user != null)
        //        {
        //            var userManager = new UserManager<AppUser>(/* provide dependencies here */); // For example purposes
        //            await userManager.DeleteAsync(user);
        //        }

        //        await transaction.CommitAsync();
        //        return (true, "تم حذف ولي الأمر بنجاح.");
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        Console.WriteLine($"An error occurred during deletion: {ex.Message}");
        //        return (false, "فشل حذف ولي الأمر.");
        //    }
        //}
        public async Task AddStudentToParentAsync(ParentStudent parentStudent)
        {
            await dbContext.ParentStudents.AddAsync(parentStudent);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveStudentFromParentAsync(Guid parentId, Guid studentId)
        {
            var parentStudent = await dbContext.ParentStudents
                                              .FirstOrDefaultAsync(ps => ps.ParentId == parentId && ps.StudentId == studentId);
            if (parentStudent != null)
            {
                dbContext.ParentStudents.Remove(parentStudent);
                await dbContext.SaveChangesAsync();
            }
        }


        public async Task<List<TeacherInfoForParentDto>> GetTeachersForParentAsync(Guid parentId)
        {
            var query = dbContext.Parents
                .Where(p => p.Id == parentId)
                .SelectMany(p => p.Students)
                .SelectMany(ps => ps.Student.CurrentSection.SectionSubjects
                    .Select(ss => new TeacherInfoForParentDto
                    {
                        TeacherId = ss.Teacher.Id,
                        TeacherName = ss.Teacher.NameAr,
                        TeacherPhoto = ss.Teacher.ProfilePictureUrl,
                        SchoolName = ss.Teacher.School.NameAr,
                        GradeName = ps.Student.CurrentSection.SchoolGrade.StageGrade.Grade.Name,
                        SectionName = ps.Student.CurrentSection.Name,
                        SubjectName = ss.GradeSubject.Subject.Name,
                        StudentName = ps.Student.NameAr
                    }))
                .Distinct();

            return await query.ToListAsync();
        }
    }
}
