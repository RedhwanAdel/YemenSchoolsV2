using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSubjectsForSchoolGrade
{
    public class GetSubjectsForSchoolGradeQueryHandler : IRequestHandler<GetSubjectsForSchoolGradeQuery, Response<List<SubjectDto>>>
    {
        private readonly ISchoolRepository _repository;

        public GetSubjectsForSchoolGradeQueryHandler(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<SubjectDto>>> Handle(GetSubjectsForSchoolGradeQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _repository.GetSubjectsForSchoolGradeAsync(request.SchoolGradeId);
            return new Response<List<SubjectDto>>(subjects);
        }
    }
}
