using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Features.Sections;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Update
{
    public class UpdateSectionSubjectCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public SectionSubjecUpdateDto Dto { get; set; }
        public UpdateSectionSubjectCommand(Guid id, SectionSubjecUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
