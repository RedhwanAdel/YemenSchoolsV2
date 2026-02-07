using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetTeachersForParent
{
    public class GetTeachersForParentQueryHandler : ResponseHandler, IRequestHandler<GetTeachersForParentQuery, Response<List<TeacherInfoForParentDto>>>
    {
        private readonly IParentRepository _parentRepository;

        public GetTeachersForParentQueryHandler(
            IParentRepository parentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<List<TeacherInfoForParentDto>>> Handle(GetTeachersForParentQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _parentRepository.GetTeachersForParentAsync(request.ParentId);
            return Success(teachers, "تم جلب بيانات المعلمين بنجاح");
        }
    }
}

