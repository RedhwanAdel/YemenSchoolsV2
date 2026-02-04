using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.ParentExists
{
    public class ParentExistsQueryHandler : IRequestHandler<ParentExistsQuery, Response<bool>>
    {
        private readonly IParentRepository _parentRepository;

        public ParentExistsQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<bool>> Handle(ParentExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _parentRepository.ParentExistsByNationalIdAsync(request.NationalId);
            return new Response<bool>(exists, exists ? "ولي الأمر موجود." : "ولي الأمر غير موجود.")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
