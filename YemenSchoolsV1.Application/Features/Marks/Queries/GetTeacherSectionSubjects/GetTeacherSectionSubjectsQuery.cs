using MediatR;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects
{
    public class GetTeacherSectionSubjectsQuery : IRequest<IEnumerable<SectionSubjectDto>>
    {
        public Guid TeacherId { get; set; }
    }
}
