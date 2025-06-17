using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Create
{
	public class CreateSubjectCommand : IRequest<Response<string>>
	{
		public Guid SchoolId { get; set; }

		public string NameAr { get; set; } = string.Empty;
		public string NameEn { get; set; } = string.Empty;
	}
}
