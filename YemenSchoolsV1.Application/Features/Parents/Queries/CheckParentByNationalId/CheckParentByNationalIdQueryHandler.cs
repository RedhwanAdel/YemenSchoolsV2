using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.CheckParentByNationalId
{
    public class CheckParentByNationalIdQueryHandler : ResponseHandler, IRequestHandler<CheckParentByNationalIdQuery, Response<ParentCheckDto>>
    {
        private readonly IParentRepository _parentRepository;

        public CheckParentByNationalIdQueryHandler(
            IParentRepository parentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<ParentCheckDto>> Handle(CheckParentByNationalIdQuery request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetParentByNationalIdAsync(request.NationalId);

            var result = parent == null 
                ? new ParentCheckDto { Exists = false }
                : new ParentCheckDto
                {
                    Id = parent.Id,
                    NameAr = parent.NameAr,
                    Exists = true
                };

            return Success(result, result.Exists ? "ولي الأمر موجود." : "ولي الأمر غير موجود.");
        }
    }
}

