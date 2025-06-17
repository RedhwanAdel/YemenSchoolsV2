using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId
{
	public class GetTeachersListQuery : IRequest<Response<List<GetTeachersListResponse>>>
	{
		public GetTeachersListQuery(Guid SchoolId)
		{
			this.SchoolId = SchoolId;
		}

		public Guid SchoolId { get; set; }  // أضفنا معرف المدرسة
	}
}