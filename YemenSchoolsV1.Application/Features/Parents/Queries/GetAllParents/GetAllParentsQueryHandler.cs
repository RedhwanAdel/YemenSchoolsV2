using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetAllParents
{
    public class GetAllParentsQueryHandler : ResponseHandler, IRequestHandler<GetAllParentsQuery, Response<IEnumerable<Parent>>>
    {
        private readonly IParentRepository _parentRepository;

        public GetAllParentsQueryHandler(
            IParentRepository parentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<IEnumerable<Parent>>> Handle(GetAllParentsQuery request, CancellationToken cancellationToken)
        {
            var parents = await _parentRepository.GetAllParentsAsync();
            return Success(parents, "تم جلب جميع أولياء الأمور بنجاح");
        }
    }
}

