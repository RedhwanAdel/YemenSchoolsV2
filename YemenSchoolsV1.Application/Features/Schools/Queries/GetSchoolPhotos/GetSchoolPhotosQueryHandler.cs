using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolPhotos
{
    public class GetSchoolPhotosQueryHandler : ResponseHandler, IRequestHandler<GetSchoolPhotosQuery, Response<List<SchoolPhoto>>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolPhotosQueryHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<List<SchoolPhoto>>> Handle(GetSchoolPhotosQuery request, CancellationToken cancellationToken)
        {
            var photos = await _repository.GetSchoolPhotosAsync(request.SchoolId);
            if (photos is null)
            {
                return NotFound<List<SchoolPhoto>>();
            }
            return Success(photos);
        }
    }
}
