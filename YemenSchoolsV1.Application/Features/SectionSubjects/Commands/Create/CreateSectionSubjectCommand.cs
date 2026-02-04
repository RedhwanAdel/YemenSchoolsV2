using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Create
{
    public class CreateSectionSubjectCommand : IRequest<Response<string>>
    {
        public CreateSectionSubjectDto Dto { get; set; }
        public CreateSectionSubjectCommand(CreateSectionSubjectDto dto) => Dto = dto;
    }
}
