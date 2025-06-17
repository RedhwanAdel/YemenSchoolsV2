using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsPaged
{
	public class GetStudentsPagedQueryHandler : ResponseHandler, IRequestHandler<GetStudentsPagedQuery, PaginatedResult<StudentDto>>
	{


		#region faild

		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public GetStudentsPagedQueryHandler(IStudentRepository studentRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;

		}

		#endregion

		public async Task<PaginatedResult<StudentDto>> Handle(GetStudentsPagedQuery request, CancellationToken cancellationToken)
		{
			var result = await studentRepository.GetPagedAsync(
				paginationQuery: request.Pagination,
				predicate: x => x.SchoolId == request.SchoolId,
				orderBy: q => q.OrderBy(s => s.NameEn));

			return PaginatedResult<StudentDto>.Success(
				mapper.Map<List<StudentDto>>(result.Data),
				result.TotalRecords,
				result.PageNumber,
				result.PageSize);
		}
	}
}
