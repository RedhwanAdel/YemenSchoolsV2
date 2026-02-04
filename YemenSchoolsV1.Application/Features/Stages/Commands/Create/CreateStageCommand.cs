using YemenSchoolsV1.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Create
{
    public class CreateStageCommand : IRequest<Response<string>>
    {
        public required string Name { get; set; }
    }
}
