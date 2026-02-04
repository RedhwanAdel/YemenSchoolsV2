using MediatR;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromoteStudentsCommandHandler : IRequestHandler<PromoteStudentsCommand, (bool Succeeded, string Message)>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository; // Note: Keeping original typo "Repository" to match interface if that's what it is, will verify
        private readonly ILogger<PromoteStudentsCommandHandler> _logger;

        public PromoteStudentsCommandHandler(
            IStudentRepository studentRepository,
            ISectionRepository sectionRepository,
            ILogger<PromoteStudentsCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        public async Task<(bool Succeeded, string Message)> Handle(PromoteStudentsCommand request, CancellationToken cancellationToken)
        {
            if (request.StudentIds == null || !request.StudentIds.Any())
            {
                _logger.LogWarning("No student IDs provided for promotion.");
                return (false, "لم يتم تحديد أي طلاب للترقية.");
            }

            var newSection = await _sectionRepository.GetSectionByIdAsync(request.NewSectionId);
            if (newSection == null)
            {
                _logger.LogWarning("New section with Id {SectionId} not found.", request.NewSectionId);
                return (false, "الشعبة الجديدة غير موجودة.");
            }

            var firstStudent = await _studentRepository.GetByIdAsync(request.StudentIds.First());
            if (firstStudent == null)
            {
                _logger.LogWarning("First student with Id {StudentId} not found.", request.StudentIds.First());
                return (false, "الطالب الأول المحدد غير موجود.");
            }

            var currentSection = await _sectionRepository.GetSectionByIdAsync(firstStudent.CurrentSectionId);
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
                await _studentRepository.PromoteStudentsAsync(request.StudentIds, newSection.AcademicYearId, request.NewSectionId);
                _logger.LogInformation("Students promoted successfully to section {SectionId}.", request.NewSectionId);
                return (true, "تمت ترقية الطلاب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to promote students to new section {SectionId}.", request.NewSectionId);
                return (false, "حدث خطأ غير متوقع أثناء ترقية الطلاب.");
            }
        }
    }
}
