using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByTeacherId
{
    public class GetSectionsByTeacherIdQuery : IRequest<Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        public Guid TeacherId { get; set; }
        public GetSectionsByTeacherIdQuery(Guid teacherId) => TeacherId = teacherId;
    }
}
