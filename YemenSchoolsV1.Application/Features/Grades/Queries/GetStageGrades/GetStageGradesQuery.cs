using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetStageGrades
{
    public class GetStageGradesQuery : IRequest<Response<List<StageGradeDto>>>
    {
    }
}
