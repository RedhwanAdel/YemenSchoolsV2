using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetById
{
	public class GetStudentByIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
	{

		#region faild

		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;

		}

		#endregion
		public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
		{
			var student = await studentRepository.GetByIdAsync(request.StudentId);
			if (student == null) return NotFound<GetStudentByIdResponse>();
			return Success(mapper.Map<GetStudentByIdResponse>(student));
		}

	}
}
