using YemenSchoolsV1.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetById
{
	public record GetStudentByIdQuery(Guid StudentId) : IRequest<Response<GetStudentByIdResponse>>;
}
