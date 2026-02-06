using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.AssignSubjects
{
    public class AssignSubjectsToSchoolGradeCommandHandler : ResponseHandler, IRequestHandler<AssignSubjectsToSchoolGradeCommand, Response<string>>
    {
        private readonly ISchoolRepository _repository;

        public AssignSubjectsToSchoolGradeCommandHandler(ISchoolRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(AssignSubjectsToSchoolGradeCommand request, CancellationToken cancellationToken)
        {
            await _repository.AssignSubjectsToSchoolGradeAsync(request.Dto.SchoolGradeId, request.Dto.SubjectIds);
            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
