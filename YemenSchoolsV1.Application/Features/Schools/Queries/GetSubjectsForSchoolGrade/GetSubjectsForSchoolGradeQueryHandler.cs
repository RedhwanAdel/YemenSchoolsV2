using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSubjectsForSchoolGrade
{
    public class GetSubjectsForSchoolGradeQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsForSchoolGradeQuery, Response<List<SubjectDto>>>
    {
        private readonly ISchoolRepository _repository;

        public GetSubjectsForSchoolGradeQueryHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<SubjectDto>>> Handle(GetSubjectsForSchoolGradeQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _repository.GetSubjectsForSchoolGradeAsync(request.SchoolGradeId);
            return Success(subjects);
        }
    }
}
