using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetStudentSubjectsReport
{
    public class GetStudentSubjectsReportQueryHandler : ResponseHandler, IRequestHandler<GetStudentSubjectsReportQuery, Response<IEnumerable<StudentSubjectReportDto>>>
    {
        private readonly IMarkRepository _markRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentSubjectsReportQueryHandler(IMarkRepository markRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _markRepository = markRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<IEnumerable<StudentSubjectReportDto>>> Handle(GetStudentSubjectsReportQuery request, CancellationToken cancellationToken)
        {
            var result = await _markRepository.GetStudentSubjectsReportAsync(request.StudentId);
            return Success(result);
        }
    }
}
