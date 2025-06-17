using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Create
{
	public class CreateGradeCommand : IRequest<Response<string>>
	{
		public Guid TermId { get; set; }
		public required string Name { get; set; }
	}
}
