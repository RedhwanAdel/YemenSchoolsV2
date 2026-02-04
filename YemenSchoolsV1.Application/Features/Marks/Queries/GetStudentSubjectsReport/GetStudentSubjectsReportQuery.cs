using MediatR;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentSubjectsReport
{
    public class GetStudentSubjectsReportQuery : IRequest<IEnumerable<StudentSubjectReportDto>>
    {
        public Guid StudentId { get; set; }
    }
}
