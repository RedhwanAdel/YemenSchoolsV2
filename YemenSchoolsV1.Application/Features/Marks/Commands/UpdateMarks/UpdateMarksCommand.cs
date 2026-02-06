using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Marks.Commands.UpdateMarks
{
    public class UpdateMarksCommand : IRequest<Response<string>>
    {
        public Guid TeacherId { get; set; }
        public Guid SectionSubjectId { get; set; }
        public string AssessmentType { get; set; } = string.Empty;
        public Dictionary<Guid, double> StudentScores { get; set; } = new();
    }
}
