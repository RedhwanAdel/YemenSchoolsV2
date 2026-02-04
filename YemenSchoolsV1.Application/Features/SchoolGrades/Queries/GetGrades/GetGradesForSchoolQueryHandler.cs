using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetGrades
{
    public class GetGradesForSchoolQueryHandler : IRequestHandler<GetGradesForSchoolQuery, Response<List<SchoolGradeDto>>>
    {
        private readonly ISchoolGradeRepository _repository;

        public GetGradesForSchoolQueryHandler(ISchoolGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<SchoolGradeDto>>> Handle(GetGradesForSchoolQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSchoolGradesBySchoolIdAsync(request.SchoolId);
            return new Response<List<SchoolGradeDto>>(result);
        }
    }
}
