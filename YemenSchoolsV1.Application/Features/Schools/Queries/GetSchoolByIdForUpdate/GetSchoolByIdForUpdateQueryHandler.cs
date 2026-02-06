using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolByIdForUpdate
{
    public class GetSchoolByIdForUpdateQueryHandler : ResponseHandler, IRequestHandler<GetSchoolByIdForUpdateQuery, Response<SchoolForUpdate>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolByIdForUpdateQueryHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<SchoolForUpdate>> Handle(GetSchoolByIdForUpdateQuery request, CancellationToken cancellationToken)
        {
            var school = await _repository.GetSchoolByIdForUpdateAsync(request.Id);
            if (school == null) return NotFound<SchoolForUpdate>();
            return Success(school);
        }
    }
}
