using System.Linq;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromoteStudentsCommandHandler : ResponseHandler, IRequestHandler<PromoteStudentsCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository; 
        private readonly ILogger<PromoteStudentsCommandHandler> _logger;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public PromoteStudentsCommandHandler(
            IStudentRepository studentRepository,
            ISectionRepository sectionRepository,
            ILogger<PromoteStudentsCommandHandler> logger,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(PromoteStudentsCommand request, CancellationToken cancellationToken)
        {
            if (request.StudentIds == null || !request.StudentIds.Any())
            {
                _logger.LogWarning("No student IDs provided for promotion.");
                return BadRequest<string>("لم يتم تحديد أي طلاب للترقية.");
            }

            var newSection = await _sectionRepository.GetSectionByIdAsync(request.NewSectionId);
            if (newSection == null)
            {
                _logger.LogWarning("New section with Id {SectionId} not found.", request.NewSectionId);
                return NotFound<string>("الشعبة الجديدة غير موجودة.");
            }

            var firstStudent = await _studentRepository.GetByIdAsync(request.StudentIds.First());
            if (firstStudent == null)
            {
                _logger.LogWarning("First student with Id {StudentId} not found.", request.StudentIds.First());
                return NotFound<string>("الطالب الأول المحدد غير موجود.");
            }

            var currentSection = await _sectionRepository.GetSectionByIdAsync(firstStudent.CurrentSectionId);
            if (currentSection == null)
            {
                _logger.LogWarning("Current section with Id {SectionId} not found for student {StudentId}.", firstStudent.CurrentSectionId, firstStudent.Id);
                return NotFound<string>("الشعبة الحالية للطالب غير موجودة.");
            }

            if (newSection.AcademicYear.StartDate <= currentSection.AcademicYear.StartDate)
            {
                _logger.LogWarning("Attempted to promote to same or previous academic year. Current: {CurrentDate}, New: {NewDate}", currentSection.AcademicYear.StartDate, newSection.AcademicYear.StartDate);
                return BadRequest<string>("لا يمكن ترقية الطلاب إلى نفس السنة أو سنة سابقة.");
            }

            try
            {
                await _studentRepository.PromoteStudentsAsync(request.StudentIds, newSection.AcademicYearId, request.NewSectionId);
                _logger.LogInformation("Students promoted successfully to section {SectionId}.", request.NewSectionId);
                return Success("تمت ترقية الطلاب بنجاح.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to promote students to new section {SectionId}.", request.NewSectionId);
                return BadRequest<string>("حدث خطأ غير متوقع أثناء ترقية الطلاب.");
            }
        }
    }
}
