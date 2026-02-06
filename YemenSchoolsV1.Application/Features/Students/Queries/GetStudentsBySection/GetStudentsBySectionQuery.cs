using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId; 

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySection
{
    public class GetStudentsBySectionQuery : IRequest<Response<List<StudentListDto>>>
    {
        public Guid SectionId { get; set; }
    }
}
