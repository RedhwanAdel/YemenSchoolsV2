using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : ResponseHandler, IRequestHandler<CreateStudentCommand, Response<Guid>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CreateStudentCommandHandler> _logger;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IParentRepository parentRepository,
            UserManager<AppUser> userManager,
            ILogger<CreateStudentCommandHandler> logger,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _userManager = userManager;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود رقم تسجيل مكرر
            if (await _studentRepository.StudentExistsByRegisterNoAsync(request.RegisterNo))
            {
                _logger.LogWarning("Attempted to create a duplicate student with RegisterNo: {RegisterNo}", request.RegisterNo);
                return BadRequest<Guid>("يوجد طالب بنفس رقم التسجيل.");
            }

            try
            {
                // 1. إنشاء حساب مستخدم (AppUser) للطالب
                var studentUser = new AppUser
                {
                    UserName = $"S-{request.RegisterNo}",
                    Name = request.NameAr,
                    Email = request.Email,
                    UserType = "Student",
                    EmailConfirmed = true,
                };

                var tempPassword = GenerateRandomPassword();
                var userCreationResult = await _userManager.CreateAsync(studentUser, tempPassword);

                if (!userCreationResult.Succeeded)
                {
                    var errors = string.Join(", ", userCreationResult.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create user account for student. Errors: {Errors}", errors);
                    return BadRequest<Guid>($"فشل إنشاء حساب المستخدم للطالب: {errors}");
                }

                // 2. إنشاء كائن الطالب وربطه بحساب المستخدم
                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    RegisterNo = request.RegisterNo,
                    NameAr = request.NameAr,
                    NameEn = request.NameEn,
                    Nationality = request.Nationality,
                    Address = request.Address,
                    Gender = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    IsActive = true,
                    CreatedTime = DateTime.UtcNow,
                    SchoolId = request.SchoolId,
                    CurrentAcademicYearId = request.CurrentAcademicYearId,
                    CurrentSectionId = request.CurrentSectionId,
                    UserId = studentUser.Id
                };

                await _studentRepository.AddAsync(student);

                // تحديث EntityId في حساب المستخدم بعد إنشاء الطالب
                studentUser.EntityId = student.Id;
                await _userManager.UpdateAsync(studentUser);

                // 3. ربط الطالب بأولياء الأمور إذا تم توفيرهم
                if (request.Parents != null && request.Parents.Any())
                {
                    foreach (var parentAssociation in request.Parents)
                    {
                        // التحقق من وجود ولي الأمر قبل الربط
                        var parentExists = await _parentRepository.GetByIdAsync(parentAssociation.ParentId);
                        if (parentExists == null)
                        {
                            _logger.LogError("Parent with Id: {ParentId} not found while creating student", parentAssociation.ParentId);
                            // نكتفي بتسجيل الخطأ هنا وعدم إفشال العملية بالكامل، أو تعديل المنطق حسب الحاجة
                            continue; 
                        }

                        var parentStudent = new ParentStudent
                        {
                            ParentId = parentAssociation.ParentId,
                            StudentId = student.Id,
                            RelationType = parentAssociation.RelationType
                        };
                        await _studentRepository.AddParentToStudentAsync(parentStudent);
                    }
                }

                _logger.LogInformation("Student and user account created successfully for RegisterNo: {RegisterNo}. LoginId: {LoginId}, TempPassword: {TempPassword}", request.RegisterNo, studentUser.UserName, tempPassword);

                return Created(student.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create student with RegisterNo: {RegisterNo}", request.RegisterNo);
                return BadRequest<Guid>("فشل إنشاء الطالب. حدث خطأ غير متوقع.");
            }
        }

        private static string GenerateRandomPassword()
        {
            return "Pa$$w0rd" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
