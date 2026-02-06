using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Commands.Sync
{
    public class SyncSchoolStageGradesCommandHandler : ResponseHandler, IRequestHandler<SyncSchoolStageGradesCommand, Response<string>>
    {
        private readonly ISchoolGradeRepository _repository;

        public SyncSchoolStageGradesCommandHandler(ISchoolGradeRepository repository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(SyncSchoolStageGradesCommand request, CancellationToken cancellationToken)
        {
            await _repository.SyncSchoolStageGradesAsync(request.Dto.SchoolId, request.Dto.StageGradeIds);
            return Success<string>("تم حفظ إعدادات الصفوف والمراحل بنجاح.");
        }
    }
}
