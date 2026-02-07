using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetParentWithStudents
{
    public class GetParentWithStudentsQueryHandler : ResponseHandler, IRequestHandler<GetParentWithStudentsQuery, Response<ParentWithStudentsDto>>
    {
        private readonly IParentRepository _parentRepository;

        public GetParentWithStudentsQueryHandler(
            IParentRepository parentRepository,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<ParentWithStudentsDto>> Handle(GetParentWithStudentsQuery request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetParentByIdWithStudentsAsync(request.ParentId);
            if (parent == null)
            {
                return NotFound<ParentWithStudentsDto>("ولي الأمر غير موجود.");
            }

            var studentDtos = parent.Students
                .Select(ps => new StudentSummaryDto
                {
                    StudentId = ps.StudentId,
                    StudentName = ps.Student.NameAr,
                    RelationType = ps.RelationType
                }).ToList();

            var result = new ParentWithStudentsDto
            {
                Id = parent.Id,
                NationalId = parent.NationalId,
                NameAr = parent.NameAr,
                NameEn = parent.NameEn,
                PhoneNumber = parent.PhoneNumber,
                Email = parent.Email,
                Address = parent.Address,
                JobTitle = parent.JobTitle,
                DateOfBirth = parent.DateOfBirth,
                IsActive = parent.IsActive,
                Students = studentDtos
            };

            return Success(result, "تم جلب بيانات ولي الأمر بنجاح");
        }
    }
}

