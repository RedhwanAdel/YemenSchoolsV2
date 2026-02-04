using MediatR;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsByAcademicYearAndSection
{
    public class GetStudentsByAcademicYearAndSectionQuery : IRequest<IEnumerable<StudentListDto>>
    {
        public Guid AcademicYearId { get; set; }
        public Guid SectionId { get; set; }
    }
}
