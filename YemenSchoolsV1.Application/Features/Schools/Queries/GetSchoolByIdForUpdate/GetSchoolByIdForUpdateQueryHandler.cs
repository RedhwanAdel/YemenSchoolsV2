using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolByIdForUpdate
{
    public class GetSchoolByIdForUpdateQueryHandler : IRequestHandler<GetSchoolByIdForUpdateQuery, Response<SchoolForUpdate>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolByIdForUpdateQueryHandler(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolForUpdate>> Handle(GetSchoolByIdForUpdateQuery request, CancellationToken cancellationToken)
        {
            var school = await _repository.GetSchoolByIdForUpdateAsync(request.Id);
            if (school == null) return new Response<SchoolForUpdate>("School not found") { Succeeded = false, StatusCode = System.Net.HttpStatusCode.NotFound };
            return new Response<SchoolForUpdate>(school);
        }
    }
}
