using Microsoft.AspNetCore.Identity;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly UserManager<AppUser> _userManager;

        public TeacherService(ITeacherRepository teacherRepository, UserManager<AppUser> userManager)
        {
            this.teacherRepository = teacherRepository;
            _userManager = userManager;
        }

        public async Task<Teacher?> CreateTeacherAsync(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }
            var user = new AppUser
            {
                UserName = teacher.Email,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Name = teacher.NameAr,
                UserType = "Teacher"
            };
            var userResult = await _userManager.CreateAsync(user, "Pa$$w0rd");
            if (!userResult.Succeeded)
                return null;
            teacher.Id = Guid.NewGuid();
            teacher.UserId = user.Id;
            user.EntityId = teacher.Id;

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return null;
            }
            return await teacherRepository.AddAsync(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            var teacher = await teacherRepository.GetByIdAsync(id);
            if (teacher == null)
                return false;
            return await teacherRepository.DeleteAsync(id);
        }

        public async Task<Teacher?> EditTeacherAsync(Guid id, Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }
            var existingTeacher = await teacherRepository.GetByIdAsync(id);
            if (existingTeacher == null) { return null; }
            return await teacherRepository.UpdateAsync(id, teacher);
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await teacherRepository.GetAllAsync();
        }

        public async Task<List<Teacher>> GetTeachersBySchoolIdAsync(Guid schoolId)
        {
            return await teacherRepository.GetAllBySchoolIdAsync(schoolId);
        }

        public async Task<Teacher?> GetTeacherDetailsAsync(Guid id)
        {
            return await teacherRepository.GetByIdAsync(id);
        }
    }
}
