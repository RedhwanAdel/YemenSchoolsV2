using MediatR;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks
{
    public class UpdateMarksCommand : IRequest<(bool Succeeded, string Message)>
    {
        public Guid TeacherId { get; set; }
        public Guid SectionSubjectId { get; set; }
        public string AssessmentType { get; set; } = string.Empty;
        public Dictionary<Guid, double> StudentScores { get; set; } = new();
    }
}
