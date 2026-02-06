using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects
{
    public class GetTeacherSectionSubjectsQuery : IRequest<Response<IEnumerable<SectionSubjectDto>>>
    {
        public Guid TeacherId { get; set; }
    }
}
