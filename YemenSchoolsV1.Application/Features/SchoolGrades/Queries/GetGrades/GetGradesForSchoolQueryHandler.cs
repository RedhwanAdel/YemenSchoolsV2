using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Queries.GetGrades
{
    public class GetGradesForSchoolQueryHandler : ResponseHandler, IRequestHandler<GetGradesForSchoolQuery, Response<List<SchoolGradeDto>>>
    {
        private readonly ISchoolGradeRepository _repository;

        public GetGradesForSchoolQueryHandler(ISchoolGradeRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<SchoolGradeDto>>> Handle(GetGradesForSchoolQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSchoolGradesBySchoolIdAsync(request.SchoolId);
            return Success(result);
        }
    }
}
