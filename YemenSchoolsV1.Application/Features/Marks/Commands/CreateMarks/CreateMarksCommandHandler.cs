using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.CreateMarks
{
    public class CreateMarksCommandHandler : ResponseHandler, IRequestHandler<CreateMarksCommand, Response<string>>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CreateMarksCommandHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(CreateMarksCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(request.SectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != request.TeacherId)
            {
                return Unauthorized<string>("Teacher is not authorized to add marks for this subject and section.");
            }

            // التحقق من وجود درجات مسبقة
            var existingMarks = await _markRepository.GetMarksBySectionSubjectAsync(request.SectionSubjectId);
            if (existingMarks.Any(m => m.AssessmentType == request.AssessmentType))
            {
                return BadRequest<string>("Marks for this assessment type already exist. Use the update method instead.");
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
            return Success("Marks created successfully.");
        }
    }
}
