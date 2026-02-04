using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.AssignSubjects
{
    public class AssignSubjectsToSchoolGradeCommand : IRequest<Response<string>>
    {
        public AssignSubjectsToSchoolGradeDto Dto { get; set; }
        public AssignSubjectsToSchoolGradeCommand(AssignSubjectsToSchoolGradeDto dto) => Dto = dto;
    }
}
