using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetStageGrades
{
    public class GetStageGradesForSchoolQuery : IRequest<Response<List<StageGradeDto>>>
    {
        public Guid SchoolId { get; set; }
        public GetStageGradesForSchoolQuery(Guid schoolId) => SchoolId = schoolId;
    }
}
