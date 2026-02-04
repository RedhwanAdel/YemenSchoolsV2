using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolPhotos
{
    public class GetSchoolPhotosQueryHandler : IRequestHandler<GetSchoolPhotosQuery, Response<List<SchoolPhoto>>>
    {
        private readonly ISchoolRepository _repository;

        public GetSchoolPhotosQueryHandler(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<SchoolPhoto>>> Handle(GetSchoolPhotosQuery request, CancellationToken cancellationToken)
        {
            var photos = await _repository.GetSchoolPhotosAsync(request.SchoolId);
            return new Response<List<SchoolPhoto>>(photos);
        }
    }
}
