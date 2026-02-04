using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.CreateMarks
{
    public class CreateMarksCommandHandler : IRequestHandler<CreateMarksCommand, (bool Succeeded, string Message)>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;

        public CreateMarksCommandHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
        }

        public async Task<(bool Succeeded, string Message)> Handle(CreateMarksCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(request.SectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != request.TeacherId)
            {
                return (false, "Teacher is not authorized to add marks for this subject and section.");
            }

            // التحقق من وجود درجات مسبقة
            var existingMarks = await _markRepository.GetMarksBySectionSubjectAsync(request.SectionSubjectId);
            if (existingMarks.Any(m => m.AssessmentType == request.AssessmentType))
            {
                return (false, "Marks for this assessment type already exist. Use the update method instead.");
            }

            // بناء قائمة الدرجات
            var marks = request.StudentScores.Select(s => new Mark
            {
                StudentId = s.Key,
                SectionSubjectId = request.SectionSubjectId,
                Score = s.Value,
                AssessmentType = request.AssessmentType,
                MaxScore = request.MaxScore
            }).ToList();

            await _markRepository.AddMarksAsync(marks);
            return (true, "Marks created successfully.");
        }
    }
}
