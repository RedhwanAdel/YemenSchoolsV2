using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentSubjectsReport
{
    public class GetStudentSubjectsReportQuery : IRequest<Response<IEnumerable<StudentSubjectReportDto>>>
    {
        public Guid StudentId { get; set; }
    }
}
