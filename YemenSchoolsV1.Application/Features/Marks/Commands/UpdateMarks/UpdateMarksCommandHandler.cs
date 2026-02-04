using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks
{
    public class UpdateMarksCommandHandler : IRequestHandler<UpdateMarksCommand, (bool Succeeded, string Message)>
    {
        private readonly IMarkRepository _markRepository;
        private readonly ISectionSubjectRepository _sectionSubjectRepository;

        public UpdateMarksCommandHandler(
            IMarkRepository markRepository,
            ISectionSubjectRepository sectionSubjectRepository)
        {
            _markRepository = markRepository;
            _sectionSubjectRepository = sectionSubjectRepository;
        }

        public async Task<(bool Succeeded, string Message)> Handle(UpdateMarksCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صلاحية المعلم
            var sectionSubject = await _sectionSubjectRepository.GetByIdAsync(request.SectionSubjectId);
            if (sectionSubject == null || sectionSubject.TeacherId != request.TeacherId)
            {
                return (false, "Teacher is not authorized to update marks for this subject and section.");
            }

            // جلب الدرجات الموجودة
            var existingMarks = (await _markRepository.GetMarksBySectionSubjectAsync(request.SectionSubjectId))
                                                      .Where(m => m.AssessmentType == request.AssessmentType)
                                                      .ToDictionary(m => m.StudentId);

            if (!existingMarks.Any())
            {
                return (false, "No existing marks found for this assessment type.");
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

            return (true, "Marks updated successfully.");
        }
    }
}
