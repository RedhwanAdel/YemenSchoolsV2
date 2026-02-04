using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetAllParents
{
    public class GetAllParentsQueryHandler : IRequestHandler<GetAllParentsQuery, Response<IEnumerable<Parent>>>
    {
        private readonly IParentRepository _parentRepository;

        public GetAllParentsQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<IEnumerable<Parent>>> Handle(GetAllParentsQuery request, CancellationToken cancellationToken)
        {
            var parents = await _parentRepository.GetAllParentsAsync();
            return new Response<IEnumerable<Parent>>(parents, "تم جلب جميع أولياء الأمور بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
