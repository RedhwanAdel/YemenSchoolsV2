using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks
{
    public class UpdateMarksCommandHandler : ResponseHandler, IRequestHandler<UpdateMarksCommand, Response<string>>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateMarksCommandHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(UpdateMarksCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(request.SectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != request.TeacherId)
            {
                return Unauthorized<string>("Teacher is not authorized to update marks for this subject and section.");
            }

            // جلب الدرجات الموجودة
            var existingMarks = (await _markRepository.GetMarksBySectionSubjectAsync(request.SectionSubjectId))
                                                      .Where(m => m.AssessmentType == request.AssessmentType)
                                                      .ToDictionary(m => m.StudentId);

            if (!existingMarks.Any())
            {
                return BadRequest<string>("No existing marks found for this assessment type.");
            }

            // تحديث الدرجات
            foreach (var score in request.StudentScores)
            {
                if (existingMarks.TryGetValue(score.Key, out var markToUpdate))
                {
                    markToUpdate.Score = score.Value;
                    await _markRepository.UpdateAsync(markToUpdate.Id, markToUpdate);
                }
            }

            return Success("Marks updated successfully.");
        }
    }
}
