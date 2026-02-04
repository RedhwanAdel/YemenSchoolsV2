using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetStageGrades
{
    public class GetStageGradesForSchoolQueryHandler : IRequestHandler<GetStageGradesForSchoolQuery, Response<List<StageGradeDto>>>
    {
        private readonly ISchoolGradeRepository _repository;

        public GetStageGradesForSchoolQueryHandler(ISchoolGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<StageGradeDto>>> Handle(GetStageGradesForSchoolQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetStageGradesAsync(request.SchoolId);
            return new Response<List<StageGradeDto>>(result);
        }
    }
}
