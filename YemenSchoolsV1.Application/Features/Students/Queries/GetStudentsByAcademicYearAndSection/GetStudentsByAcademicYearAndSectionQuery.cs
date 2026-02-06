using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsByAcademicYearAndSection
{
    public class GetStudentsByAcademicYearAndSectionQuery : IRequest<Response<List<StudentListDto>>>
    {
        public Guid AcademicYearId { get; set; }
        public Guid SectionId { get; set; }
    }
}
