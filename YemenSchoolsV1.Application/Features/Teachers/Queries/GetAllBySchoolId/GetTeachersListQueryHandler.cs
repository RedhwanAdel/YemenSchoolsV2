using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetAllBySchoolId
{
	public class GetTeachersListQueryHandler : ResponseHandler, IRequestHandler<GetTeachersListQuery, Response<List<GetTeachersListResponse>>>
	{
		#region الفيلدز

		private readonly ITeacherService teacherService;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;

		#endregion الفيلدز

		#region المُنشئ

		public GetTeachersListQueryHandler(ITeacherService teacherService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
			: base(stringLocalizer)
		{
			this.teacherService = teacherService;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;
		}

		#endregion المُنشئ

		#region المعالج

		public async Task<Response<List<GetTeachersListResponse>>> Handle(GetTeachersListQuery request, CancellationToken cancellationToken)
		{
			var teachers = await teacherService.GetTeachersBySchoolIdAsync(request.SchoolId);  // استخدمنا الفلترة حسب المدرسة
			var response = mapper.Map<List<GetTeachersListResponse>>(teachers);
			return Success(response);
		}

		#endregion المعالج
	}
}