using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetGrades
{
    public class GetGradesForSchoolQuery : IRequest<Response<List<SchoolGradeDto>>>
    {
        public Guid SchoolId { get; set; }
        public GetGradesForSchoolQuery(Guid schoolId) => SchoolId = schoolId;
    }
}
