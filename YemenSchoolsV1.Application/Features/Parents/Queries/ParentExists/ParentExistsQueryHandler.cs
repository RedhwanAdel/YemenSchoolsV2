using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.ParentExists
{
    public class ParentExistsQueryHandler : ResponseHandler, IRequestHandler<ParentExistsQuery, Response<bool>>
    {
        private readonly IParentRepository _parentRepository;

        public ParentExistsQueryHandler(
            IParentRepository parentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<bool>> Handle(ParentExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _parentRepository.ParentExistsByNationalIdAsync(request.NationalId);
            return Success(exists, exists ? "ولي الأمر موجود." : "ولي الأمر غير موجود.");
        }
    }
}
