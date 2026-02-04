using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSubjectsForSchoolGrade
{
    public class GetSubjectsForSchoolGradeQuery : IRequest<Response<List<SubjectDto>>>
    {
        public Guid SchoolGradeId { get; set; }
        public GetSubjectsForSchoolGradeQuery(Guid schoolGradeId) => SchoolGradeId = schoolGradeId;
    }
}
