using Microsoft.AspNetCore.Identity;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents
{
    public class ParentService : IParentService
    {
        private readonly IParentRepositry _parentRepository;
        private readonly UserManager<AppUser> _userManager;

        public ParentService(IParentRepositry parentRepository, UserManager<AppUser> userManager)
        {
            _parentRepository = parentRepository;
            _userManager = userManager;
        }

        public async Task<(bool Succeeded, string Message)> CreateParentWithUserAsync(ParentCreateDto dto, string defaultPassword)
        {
            if (await _parentRepository.ParentExistsByNationalIdAsync(dto.NationalId))
                return (false, "يوجد ولي أمر بنفس رقم الهوية.");


            // إنشاء المستخدم في الهوية
            var user = new AppUser
            {
                UserName = dto.NationalId,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.NameAr, // Set custom fields
                UserType = "Parent"

            };
            var userResult = await _userManager.CreateAsync(user, defaultPassword);
            if (!userResult.Succeeded)
                return (false, string.Join("; ", userResult.Errors.Select(e => e.Description)));

            // إنشاء ولي الأمر وربطه بالمستخدم
            var parent = new Parent
            {
                Id = Guid.NewGuid(),
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                NationalId = dto.NationalId,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                DateOfBirth = dto.DateOfBirth,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
            };
            user.EntityId = parent.Id;
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                // If updating the user fails, we must roll back the user creation to avoid orphaned data.
                await _userManager.DeleteAsync(user);
                return (false, "فشل تحديث حساب المستخدم. تم إلغاء العملية.");
            }

            try
            {
                // Using a repository method (assuming it uses SaveChanges)
                await _parentRepository.AddAsync(parent);
                return (true, "تم إنشاء ولي الأمر والمستخدم بنجاح.");
            }
            catch
            {
                // CRITICAL: If adding the parent fails, we must delete the previously created user account.
                await _userManager.DeleteAsync(user);
                return (false, "فشل إنشاء ولي الأمر. تم إلغاء حساب المستخدم.");
            }
        }

        public async Task<(bool Succeeded, string Message)> UpdateParentProfileAsync(Guid userId, ParentUpdateDto dto)
        {
            try
            {
                // 1. البحث عن حساب المستخدم (AppUser)
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return (false, "لم يتم العثور على حساب المستخدم.");
                }

                // 2. البحث عن كيان ولي الأمر (Parent) المرتبط
                // نفترض وجود دالة في Repository تقوم بهذا الأمر
                var parent = await _parentRepository.GetParentByUserIdAsync(userId);
                if (parent == null)
                {
                    return (false, "لم يتم العثور على بيانات ولي الأمر.");
                }

                // 3. تحديث الحقول في كيان Parent من الـ DTO
                parent.NameAr = dto.NameAr;
                parent.NameEn = dto.NameEn;
                parent.PhoneNumber = dto.PhoneNumber;
                parent.Address = dto.Address;
                parent.Email = dto.Email;
                parent.JobTitle = dto.JobTitle;

                // 4. تحديث الحقول في AppUser
                // يجب تحديث AppUser أيضاً لأن بعض الحقول قد تكون مشتركة
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;

                // 5. حفظ التغييرات في قاعدة البيانات
                // حفظ التغييرات في AppUser
                var userUpdateResult = await _userManager.UpdateAsync(user);
                if (!userUpdateResult.Succeeded)
                {
                    var errors = string.Join("; ", userUpdateResult.Errors.Select(e => e.Description));
                    return (false, $"فشل تحديث حساب المستخدم: {errors}");
                }

                // حفظ التغييرات في Parent باستخدام الـ Repository
                await _parentRepository.UpdateAsync(parent.Id, parent);

                return (true, "تم تحديث الملف الشخصي بنجاح.");
            }
            catch (Exception ex)
            {
                return (false, "حدث خطأ غير متوقع أثناء تحديث البيانات.");
            }
        }


        public async Task<ParentWithStudentsDto?> GetParentWithStudentsAsync(Guid parentId)
        {
            var parent = await _parentRepository.GetParentByIdWithStudentsAsync(parentId);
            if (parent == null)
            {
                return null;
            }

            var studentDtos = parent.Students
                .Select(ps => new StudentSummaryDto
                {
                    StudentId = ps.StudentId,
                    StudentName = ps.Student.NameAr,
                    RelationType = ps.RelationType
                }).ToList();

            return new ParentWithStudentsDto
            {
                Id = parent.Id,
                NationalId = parent.NationalId,
                NameAr = parent.NameAr,
                NameEn = parent.NameEn,
                PhoneNumber = parent.PhoneNumber,
                Email = parent.Email,
                Address = parent.Address,
                JobTitle = parent.JobTitle,
                DateOfBirth = parent.DateOfBirth,
                IsActive = parent.IsActive,
                Students = studentDtos
            };
        }

        public async Task<ParentWithStudentsDto?> GetParentProfileAsync(Guid userId)
        {
            var parent = await _parentRepository.GetParentByUserIdAsync(userId);
            if (parent == null)
            {
                return null;
            }

            // We can reuse the GetParentWithStudentsAsync method for this
            return await GetParentWithStudentsAsync(parent.Id);
        }

        public async Task<IEnumerable<Parent>> GetAllParentsAsync()
        {
            return await _parentRepository.GetAllParentsAsync();
        }
        public async Task<(bool Succeeded, string Message)> DeactivateParentAsync(Guid parentId)
        {
            try
            {
                await _parentRepository.DeactivateParentAsync(parentId);
                return (true, "تم تعطيل حساب ولي الأمر بنجاح.");
            }
            catch
            {
                return (false, "فشل تعطيل حساب ولي الأمر.");
            }
        }

        //public async Task<(bool Succeeded, string Message)> DeleteParentAsync(Guid parentId)
        //{
        //    var parent = await _parentRepository.GetParentByIdWithStudentsAsync(parentId);
        //    if (parent == null)
        //    {
        //        return (false, "ولي الأمر غير موجود.");
        //    }

        //    // The repository will handle the cascading delete of the user and relationships.
        //    var result = await _parentRepository.DeleteParentAndRelatedDataAsync(parent);

        //    if (!result.Succeeded)
        //    {
        //    }

        //    return result;
        //}
        public async Task<(bool Succeeded, string Message)> AddStudentToParentAsync(Guid parentId, Guid studentId, string relationType)
        {
            // Add checks to ensure the parent and student actually exist.
            var parent = await _parentRepository.GetParentByIdWithStudentsAsync(parentId);
            // var student = await _studentRepository.GetStudentByIdAsync(studentId); // Assuming a student repository exists

            if (parent == null /* || student == null */)
            {
                return (false, "البيانات المدخلة غير صحيحة.");
            }

            var newParentStudent = new ParentStudent
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                StudentId = studentId,
                RelationType = relationType
            };

            try
            {
                await _parentRepository.AddStudentToParentAsync(newParentStudent);
                return (true, "تم ربط الطالب بولي الأمر بنجاح.");
            }
            catch
            {
                return (false, "فشل ربط الطالب بولي الأمر.");
            }
        }
        public async Task<(bool Succeeded, string Message)> RemoveStudentFromParentAsync(Guid parentId, Guid studentId)
        {
            try
            {
                await _parentRepository.RemoveStudentFromParentAsync(parentId, studentId);
                return (true, "تم إزالة علاقة الطالب بولي الأمر بنجاح.");
            }
            catch
            {
                return (false, "فشل إزالة علاقة الطالب بولي الأمر.");
            }
        }

    }
}
