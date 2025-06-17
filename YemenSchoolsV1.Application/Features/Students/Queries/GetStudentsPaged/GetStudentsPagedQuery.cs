using MediatR;
using System.ComponentModel.DataAnnotations;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsPaged
{
	public class GetStudentsPagedQuery : IRequest<PaginatedResult<StudentDto>>
	{
		public PaginationQuery Pagination { get; set; }
		[Required]
		public Guid SchoolId { get; set; }

		public GetStudentsPagedQuery(PaginationQuery pagination, Guid schoolId)
		{
			Pagination = pagination;
			SchoolId = schoolId;
		}
	}
}
