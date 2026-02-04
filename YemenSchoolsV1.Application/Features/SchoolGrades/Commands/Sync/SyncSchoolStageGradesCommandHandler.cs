using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Commands.Sync
{
    public class SyncSchoolStageGradesCommandHandler : IRequestHandler<SyncSchoolStageGradesCommand, Response<string>>
    {
        private readonly ISchoolGradeRepository _repository;

        public SyncSchoolStageGradesCommandHandler(ISchoolGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<string>> Handle(SyncSchoolStageGradesCommand request, CancellationToken cancellationToken)
        {
            await _repository.SyncSchoolStageGradesAsync(request.Dto.SchoolId, request.Dto.StageGradeIds);
            return new Response<string>("تم حفظ إعدادات الصفوف والمراحل بنجاح.") { Succeeded = true, StatusCode = System.Net.HttpStatusCode.OK };
        }
    }
}
