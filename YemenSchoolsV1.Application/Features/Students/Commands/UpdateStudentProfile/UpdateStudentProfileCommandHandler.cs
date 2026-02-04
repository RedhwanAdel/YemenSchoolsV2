using MediatR;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Students.Commands.UpdateStudentProfile
{
    public class UpdateStudentProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, (bool Succeeded, string Message)>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<UpdateStudentProfileCommandHandler> _logger;

        public UpdateStudentProfileCommandHandler(
            IStudentRepository studentRepository,
            ILogger<UpdateStudentProfileCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<(bool Succeeded, string Message)> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(request.StudentId);
                if (student == null)
                {
                    return (false, "الطالب غير موجود.");
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
                return (true, "تم تحديث بيانات الطالب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update student profile for StudentId {StudentId}", request.StudentId);
                return (false, "حدث خطأ غير متوقع أثناء تحديث البيانات.");
            }
        }
    }
}
