using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.AssignSubjects
{
    public class AssignSubjectsToSchoolGradeCommandHandler : IRequestHandler<AssignSubjectsToSchoolGradeCommand, Response<string>>
    {
        private readonly ISchoolRepository _repository;

        public AssignSubjectsToSchoolGradeCommandHandler(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(AssignSubjectsToSchoolGradeCommand request, CancellationToken cancellationToken)
        {
            await _repository.AssignSubjectsToSchoolGradeAsync(request.Dto.SchoolGradeId, request.Dto.SubjectIds);
            return new Response<string>("تم حفظ إعدادات المواد بنجاح.") { Succeeded = true, StatusCode = System.Net.HttpStatusCode.OK };
        }
    }
}
