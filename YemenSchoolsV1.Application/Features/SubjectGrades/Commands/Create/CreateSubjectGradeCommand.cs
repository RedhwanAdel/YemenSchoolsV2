using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.SubjectGrades.Commands.Create
{
	public class CreateSubjectGradeCommand : IRequest<Response<string>>
	{
		public Guid SubjectId { get; set; }
		public Guid GradeId { get; set; }
		public decimal MinPassMark { get; set; }
		public decimal MaxMark { get; set; }
	}
}
