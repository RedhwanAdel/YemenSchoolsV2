using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto.Parents;

namespace YemenSchoolsV1.Application.Features.Parents.Queries.GetStudentsWithSchoolInfo
{
    public class GetStudentsWithSchoolInfoQueryHandler : IRequestHandler<GetStudentsWithSchoolInfoQuery, Response<List<StudentWithSchoolInfoDto>>>
    {
        private readonly IParentRepository _parentRepository;

        public GetStudentsWithSchoolInfoQueryHandler(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Response<List<StudentWithSchoolInfoDto>>> Handle(GetStudentsWithSchoolInfoQuery request, CancellationToken cancellationToken)
        {
            var students = await _parentRepository.GetStudentsByParentIdAsync(request.ParentId);

            var result = students.Select(s => new StudentWithSchoolInfoDto
            {
                StudentId = s.Id,
                StudentName = s.NameAr,
                ImageUrl = s.ProfileImage,
                SchoolName = s.CurrentSection?.SchoolGrade?.School?.NameAr ?? "",
                ClassName = s.CurrentSection?.SchoolGrade?.StageGrade?.Grade?.Name ?? "",
                SectionName = s.CurrentSection?.Name ?? ""
            }).ToList();

            return new Response<List<StudentWithSchoolInfoDto>>(result, "تم جلب بيانات الطلاب بنجاح")
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true
            };
        }
    }
}
