using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetParentWithStudents
{
    public class GetParentWithStudentsQueryHandler : IRequestHandler<GetParentWithStudentsQuery, Response<ParentWithStudentsDto>>
    {
        private readonly IParentRepository _parentRepository;

        public GetParentWithStudentsQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<ParentWithStudentsDto>> Handle(GetParentWithStudentsQuery request, CancellationToken cancellationToken)
        {
            var parent = await _parentRepository.GetParentByIdWithStudentsAsync(request.ParentId);
            if (parent == null)
            {
                return new Response<ParentWithStudentsDto>("ولي الأمر غير موجود.", false)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
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

            return new Response<ParentWithStudentsDto>(result, "تم جلب بيانات ولي الأمر بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
