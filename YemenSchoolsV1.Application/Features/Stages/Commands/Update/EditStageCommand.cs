using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Update
{
	public class EditStageCommand : IRequest<Response<string>>
	{
		public Guid Id { get; set; }

		public string? Name { get; set; }
		public Guid SchoolId { get; set; }
	}
}
