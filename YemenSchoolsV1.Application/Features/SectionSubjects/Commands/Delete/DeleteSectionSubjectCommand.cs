using MediatR;
using YemenSchoolsV1.Application.Bases;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Commands.Delete
{
    public class DeleteSectionSubjectCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public DeleteSectionSubjectCommand(Guid id) => Id = id;
    }
}
