using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetParentProfile
{
    public class GetParentProfileQueryHandler : IRequestHandler<GetParentProfileQuery, Response<ParentWithStudentsDto>>
    {
        private readonly IParentRepository _parentRepository;

        public GetParentProfileQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<ParentWithStudentsDto>> Handle(GetParentProfileQuery request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetParentByUserIdAsync(request.UserId);
            if (parent == null)
            {
                return new Response<ParentWithStudentsDto>("لم يتم العثور على ملف ولي الأمر.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            // Reuse the logic for getting students
            var fullParent = await _parentRepository.GetParentByIdWithStudentsAsync(parent.Id);
            
            var studentDtos = fullParent.Students
                .Select(ps => new StudentSummaryDto
                {
                    StudentId = ps.StudentId,
                    StudentName = ps.Student.NameAr,
                    RelationType = ps.RelationType
                }).ToList();

            var result = new ParentWithStudentsDto
            {
                Id = fullParent.Id,
                NationalId = fullParent.NationalId,
                NameAr = fullParent.NameAr,
                NameEn = fullParent.NameEn,
                PhoneNumber = fullParent.PhoneNumber,
                Email = fullParent.Email,
                Address = fullParent.Address,
                JobTitle = fullParent.JobTitle,
                DateOfBirth = fullParent.DateOfBirth,
                IsActive = fullParent.IsActive,
                Students = studentDtos
            };

            return new Response<ParentWithStudentsDto>(result, "تم جلب بيانات ولي الأمر بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
