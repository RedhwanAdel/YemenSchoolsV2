using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Commands.UpdateStudentProfile
{
    public class UpdateStudentProfileCommandHandler : ResponseHandler, IRequestHandler<UpdateStudentProfileCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<UpdateStudentProfileCommandHandler> _logger;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateStudentProfileCommandHandler(
            IStudentRepository studentRepository,
            ILogger<UpdateStudentProfileCommandHandler> logger,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(request.StudentId);
                if (student == null)
                {
                    return NotFound<string>("الطالب غير موجود.");
                }

                // تحديث الحقول
                student.NameAr = request.NameAr;
                student.NameEn = request.NameEn;
                student.Nationality = request.Nationality;
                student.Address = request.Address;
                student.Gender = request.Gender;
                student.DateOfBirth = request.DateOfBirth;
                student.PhoneNumber = request.PhoneNumber;
                student.Email = request.Email;

                await _studentRepository.UpdateAsync(request.StudentId, student);
                return Success("تم تحديث بيانات الطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update student profile for StudentId {StudentId}", request.StudentId);
                return BadRequest<string>("حدث خطأ غير متوقع أثناء تحديث البيانات.");
            }
        }
    }
}
