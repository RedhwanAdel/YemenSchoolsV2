using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentSubjectsReport
{
    public class GetStudentSubjectsReportQueryHandler : IRequestHandler<GetStudentSubjectsReportQuery, IEnumerable<StudentSubjectReportDto>>
    {
        private readonly IMarkRepository _markRepository;

        public GetStudentSubjectsReportQueryHandler(IMarkRepository markRepository)
        {
            _markRepository = markRepository;
        }

        public async Task<IEnumerable<StudentSubjectReportDto>> Handle(GetStudentSubjectsReportQuery request, CancellationToken cancellationToken)
        {
            return await _markRepository.GetStudentSubjectsReportAsync(request.StudentId);
        }
    }
}
