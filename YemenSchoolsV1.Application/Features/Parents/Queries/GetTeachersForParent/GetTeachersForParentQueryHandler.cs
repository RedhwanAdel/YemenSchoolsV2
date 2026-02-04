using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetTeachersForParent
{
    public class GetTeachersForParentQueryHandler : IRequestHandler<GetTeachersForParentQuery, Response<List<TeacherInfoForParentDto>>>
    {
        private readonly IParentRepository _parentRepository;

        public GetTeachersForParentQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<List<TeacherInfoForParentDto>>> Handle(GetTeachersForParentQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _parentRepository.GetTeachersForParentAsync(request.ParentId);
            return new Response<List<TeacherInfoForParentDto>>(teachers, "تم جلب بيانات المعلمين بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
