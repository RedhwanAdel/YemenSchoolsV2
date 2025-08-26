using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Dto.Students;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Students
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepositry _parentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<StudentService> _logger;
        private readonly ISectionRepositry sectionRepositry;

        public StudentService(
            IStudentRepository studentRepository,
            IParentRepositry parentRepository,
            UserManager<AppUser> userManager,
            ILogger<StudentService> logger,
            ISectionRepositry sectionRepositry
            )
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _userManager = userManager;
            _logger = logger;
            this.sectionRepositry = sectionRepositry;
        }


        public async Task<(bool Succeeded, string Message)> PromoteStudentsToNewSectionAsync(List<Guid> studentIds, Guid newSectionId)
        {
            if (studentIds == null || !studentIds.Any())
            {
                _logger.LogWarning("No student IDs provided for promotion.");
                return (false, "لم يتم تحديد أي طلاب للترقية.");
            }

            var newSection = await sectionRepositry.GetSectionByIdAsync(newSectionId);
            if (newSection == null)
            {
                _logger.LogWarning("New section with Id {SectionId} not found.", newSectionId);
                return (false, "الشعبة الجديدة غير موجودة.");
            }

            var firstStudent = await _studentRepository.GetByIdAsync(studentIds.First());
            if (firstStudent == null)
            {
                _logger.LogWarning("First student with Id {StudentId} not found.", studentIds.First());
                return (false, "الطالب الأول المحدد غير موجود.");
            }

            var currentSection = await sectionRepositry.GetSectionByIdAsync(firstStudent.CurrentSectionId);
            if (currentSection == null)
            {
                _logger.LogWarning("Current section with Id {SectionId} not found for student {StudentId}.", firstStudent.CurrentSectionId, firstStudent.Id);
                return (false, "الشعبة الحالية للطالب غير موجودة.");
            }

            if (newSection.AcademicYear.StartDate <= currentSection.AcademicYear.StartDate)
            {
                _logger.LogWarning("Attempted to promote to same or previous academic year. Current: {CurrentDate}, New: {NewDate}", currentSection.AcademicYear.StartDate, newSection.AcademicYear.StartDate);
                return (false, "لا يمكن ترقية الطلاب إلى نفس السنة أو سنة سابقة.");
            }

            try
            {
                await _studentRepository.PromoteStudentsAsync(studentIds, newSection.AcademicYearId, newSectionId);
                _logger.LogInformation("Students promoted successfully to section {SectionId}.", newSectionId);
                return (true, "تمت ترقية الطلاب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to promote students to new section {SectionId}.", newSectionId);
                return (false, "حدث خطأ غير متوقع أثناء ترقية الطلاب.");
            }
        }


        /// <summary>
        /// ينشئ طالبًا جديدًا في النظام.
        /// </summary>
        /// <param name="dto">كائن نقل البيانات لإنشاء الطالب.</param>
        /// <returns>نتيجة العملية مع رسالة توضيحية.</returns>
        public async Task<(bool Succeeded, string Message)> CreateStudentAsync(StudentCreateDto dto)
        {
            // التحقق من وجود رقم تسجيل مكرر
            if (await _studentRepository.StudentExistsByRegisterNoAsync(dto.RegisterNo))
            {
                _logger.LogWarning("Attempted to create a duplicate student with RegisterNo: {RegisterNo}", dto.RegisterNo);
                return (false, "يوجد طالب بنفس رقم التسجيل.");
            }

            // استخدام transaction لضمان أن جميع العمليات تتم بنجاح أو لا تتم أبدًا
            try
            {
                // التحقق من وجود الكيانات الأساسية
                //var schoolExists = await _context.Schools.AnyAsync(s => s.Id == dto.SchoolId);
                //var academicYearExists = await _context.AcademicYears.AnyAsync(ay => ay.Id == dto.CurrentAcademicYearId);
                //var sectionExists = await _context.Sections.AnyAsync(s => s.Id == dto.CurrentSectionId);

                //if (!schoolExists)
                //{
                //    return (false, "المدرسة المحددة غير موجودة.");
                //}
                //if (!academicYearExists)
                //{
                //    return (false, "العام الدراسي المحدد غير موجود.");
                //}
                //if (!sectionExists)
                //{
                //    return (false, "القسم المحدد غير موجود.");
                //}

                // 1. إنشاء حساب مستخدم (AppUser) للطالب
                var studentUser = new AppUser
                {
                    UserName = $"S-{dto.RegisterNo}",
                    FirstName = dto.NameAr,
                    Email = dto.Email,
                    UserType = "Student",
                    EmailConfirmed = true,
                };

                var tempPassword = GenerateRandomPassword();
                var userCreationResult = await _userManager.CreateAsync(studentUser, tempPassword);

                if (!userCreationResult.Succeeded)
                {
                    var errors = string.Join(", ", userCreationResult.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create user account for student. Errors: {Errors}", errors);
                    return (false, $"فشل إنشاء حساب المستخدم للطالب: {errors}");
                }

                // 2. إنشاء كائن الطالب وربطه بحساب المستخدم
                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    RegisterNo = dto.RegisterNo,
                    NameAr = dto.NameAr,
                    NameEn = dto.NameEn,
                    Nationality = dto.Nationality,
                    Address = dto.Address,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    IsActive = true,
                    CreatedTime = DateTime.UtcNow,
                    SchoolId = dto.SchoolId,
                    CurrentAcademicYearId = dto.CurrentAcademicYearId,
                    CurrentSectionId = dto.CurrentSectionId,
                    UserId = studentUser.Id
                };

                await _studentRepository.AddAsync(student);

                // تحديث EntityId في حساب المستخدم بعد إنشاء الطالب
                studentUser.EntityId = student.Id;
                await _userManager.UpdateAsync(studentUser);

                // 3. ربط الطالب بأولياء الأمور إذا تم توفيرهم
                if (dto.Parents != null && dto.Parents.Any())
                {
                    foreach (var parentAssociation in dto.Parents)
                    {
                        // التحقق من وجود ولي الأمر قبل الربط
                        var parentExists = await _parentRepository.GetByIdAsync(parentAssociation.ParentId);
                        if (parentExists == null)
                        {
                            _logger.LogError("Parent with Id: {ParentId} not found while creating student", parentAssociation.ParentId);
                            return (false, $"ولي الأمر بالمعرّف {parentAssociation.ParentId} غير موجود.");
                        }

                        var parentStudent = new ParentStudent
                        {
                            ParentId = parentAssociation.ParentId,
                            StudentId = student.Id,
                            RelationType = parentAssociation.RelationType // استخدام نوع العلاقة المحدد من الـ DTO
                        };
                        await _studentRepository.AddParentToStudentAsync(parentStudent);
                    }
                }
                // تأكيد العملية بالكامل

                _logger.LogInformation("Student and user account created successfully for RegisterNo: {RegisterNo}. LoginId: {LoginId}, TempPassword: {TempPassword}", dto.RegisterNo, studentUser.UserName, tempPassword);

                return (true, "تم إنشاء الطالب وحسابه بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create student with RegisterNo: {RegisterNo}", dto.RegisterNo);
                return (false, "فشل إنشاء الطالب. حدث خطأ غير متوقع.");
            }
        }

        /// <summary>
        /// يجلب ملف الطالب مع جميع أولياء أموره المرتبطين به.
        /// </summary>
        public async Task<StudentWithParentsDto?> GetStudentProfileWithParentsAsync(Guid studentId)
        {
            var student = await _studentRepository.GetStudentByIdWithParentsAsync(studentId);
            if (student == null)
            {
                return null;
            }

            var parentDtos = student.Parents.Select(ps => new ParentSummaryDto
            {
                ParentId = ps.ParentId,
                NameAr = ps.Parent.NameAr,
                NameEn = ps.Parent.NameEn,
                RelationType = ps.RelationType
            }).ToList();

            return new StudentWithParentsDto
            {
                Id = student.Id,
                RegisterNo = student.RegisterNo,
                NameAr = student.NameAr,
                NameEn = student.NameEn,
                Nationality = student.Nationality,
                Address = student.Address,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                Parents = parentDtos
            };
        }

        /// <summary>
        /// يجلب قائمة الطلاب في عام دراسي وفصل محدد.
        /// </summary>
        public async Task<IEnumerable<Student>> GetStudentsByAcademicYearAndSectionAsync(Guid academicYearId, Guid sectionId)
        {
            return await _studentRepository.GetStudentsByAcademicYearAndSectionAsync(academicYearId, sectionId);
        }
        public async Task<IEnumerable<Student>> GetStudentsBySectionAsync(Guid sectionId)
        {
            return await _studentRepository.GetStudentsBySectionIdAsync(sectionId);
        }
        public async Task<IEnumerable<StudentListDto>> GetStudentsBySchoolIdAsync(Guid schoolId)
        {
            return await _studentRepository.GetStudentsBySchoolIdAsync(schoolId);
        }
        /// <summary>
        /// يقوم بتحديث بيانات ملف الطالب.
        /// </summary>
        public async Task<(bool Succeeded, string Message)> UpdateStudentProfileAsync(Guid studentId, StudentUpdateDto dto)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(studentId);
                if (student == null)
                {
                    return (false, "الطالب غير موجود.");
                }

                // تحديث الحقول المسموح بها من الـ DTO
                student.NameAr = dto.NameAr;
                student.NameEn = dto.NameEn;
                student.Nationality = dto.Nationality;
                student.Address = dto.Address;
                student.PhoneNumber = dto.PhoneNumber;
                student.Email = dto.Email;

                await _studentRepository.UpdateAsync(studentId, student);
                return (true, "تم تحديث بيانات الطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update student profile for StudentId {studentId}", studentId);
                return (false, "حدث خطأ غير متوقع أثناء تحديث البيانات.");
            }
        }

        /// <summary>
        /// يحذف طالبًا من النظام.
        /// </summary>
        //public async Task<(bool Succeeded, string Message)> DeleteStudentAsync(Guid studentId)
        //{
        //    await using var transaction = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var student = await _studentRepository.GetStudentByIdAsync(studentId);
        //        if (student != null)
        //        {
        //            var result = await _studentRepository.DeleteStudentAsync(studentId);
        //            if (result.Succeeded)
        //            {
        //                // يجب حذف حساب المستخدم المرتبط بالطالب
        //                var user = await _userManager.FindByIdAsync(student.UserId.ToString());
        //                if (user != null)
        //                {
        //                    await _userManager.DeleteAsync(user);
        //                }
        //                await transaction.CommitAsync();
        //            }
        //            return result;
        //        }
        //        return (false, "الطالب غير موجود.");
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        _logger.LogError(ex, "Failed to delete student with Id: {studentId}", studentId);
        //        return (false, "فشل حذف الطالب.");
        //    }
        //}

        /// <summary>
        /// يربط طالبًا بولي أمر جديد في جدول ParentStudent.
        /// </summary>
        public async Task<(bool Succeeded, string Message)> AddParentToStudentAsync(Guid studentId, Guid parentId, string relationType)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var parent = await _parentRepository.GetByIdAsync(parentId);

            if (student == null || parent == null)
            {
                return (false, "الطالب أو ولي الأمر غير موجود.");
            }

            var newParentStudent = new ParentStudent
            {
                StudentId = studentId,
                ParentId = parentId,
                RelationType = relationType
            };

            try
            {
                await _studentRepository.AddParentToStudentAsync(newParentStudent);
                return (true, "تم ربط ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add parent to student relationship.");
                return (false, "فشل ربط ولي الأمر بالطالب.");
            }
        }

        /// <summary>
        /// يزيل علاقة ولي أمر من طالب.
        /// </summary>
        public async Task<(bool Succeeded, string Message)> RemoveParentFromStudentAsync(Guid studentId, Guid parentId)
        {
            try
            {
                await _studentRepository.RemoveParentFromStudentAsync(studentId, parentId);
                return (true, "تم إزالة علاقة ولي الأمر بالطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove parent from student relationship.");
                return (false, "فشل إزالة علاقة ولي الأمر بالطالب.");
            }
        }

        // دالة مساعدة لتوليد كلمة مرور عشوائية
        private static string GenerateRandomPassword()
        {
            return "Pa$$w0rd" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
