using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetStageGrades
{
    public class GetStageGradesForSchoolQueryHandler : ResponseHandler, IRequestHandler<GetStageGradesForSchoolQuery, Response<List<StageGradeDto>>>
    {
        private readonly ISchoolGradeRepository _repository;

        public GetStageGradesForSchoolQueryHandler(ISchoolGradeRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<StageGradeDto>>> Handle(GetStageGradesForSchoolQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetStageGradesAsync(request.SchoolId);
            return Success(result);
        }
    }
}
