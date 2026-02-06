using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId
{
	public class GetTeachersListQueryHandler : ResponseHandler, IRequestHandler<GetTeachersListQuery, Response<List<GetTeachersListResponse>>>
	{
		private readonly ITeacherRepository _teacherRepository;
		private readonly IMapper _mapper;

		public GetTeachersListQueryHandler(ITeacherRepository teacherRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
			: base(stringLocalizer)
		{
			_teacherRepository = teacherRepository;
			_mapper = mapper;
		}

		public async Task<Response<List<GetTeachersListResponse>>> Handle(GetTeachersListQuery request, CancellationToken cancellationToken)
		{
			var teachers = await _teacherRepository.GetAllBySchoolIdAsync(request.SchoolId);
			var response = _mapper.Map<List<GetTeachersListResponse>>(teachers);
			return Success(response);
		}
	}
}
