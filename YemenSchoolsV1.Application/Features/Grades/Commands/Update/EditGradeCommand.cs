using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Update
{
	public class EditGradeCommand : IRequest<Response<string>>
	{
		public Guid Id { get; set; }
		public Guid TermId { get; set; }
		public string Name { get; set; }
	}
}
