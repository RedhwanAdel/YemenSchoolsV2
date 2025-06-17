using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails
{
	public class GetSchoolDetailsQuery : IRequest<Response<GetSchoolDetailsResponse>>
	{
		public GetSchoolDetailsQuery(Guid id)
		{
			Id = id;
		}
		public Guid Id { get; set; }

	}
}
