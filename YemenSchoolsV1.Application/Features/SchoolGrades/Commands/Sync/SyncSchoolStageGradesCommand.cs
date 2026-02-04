using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SchoolGrades.Commands.Sync
{
    public class SyncSchoolStageGradesCommand : IRequest<Response<string>>
    {
        public CreateSchoolGradeDto Dto { get; set; }
        public SyncSchoolStageGradesCommand(CreateSchoolGradeDto dto) => Dto = dto;
    }
}
