using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Students.Commands.Create
{
	public class CreateStudentCommandHandler : ResponseHandler, IRequestHandler<CreateStudentCommand, Response<string>>
	{
		#region faild

		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;
		#endregion

		#region ctor
		public CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;

		}

		#endregion
		public async Task<Response<string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
		{
			var student = await studentRepository.AddAsync(mapper.Map<Student>(request));
			if (student == null) return UnprocessableEntity<string>();
			return Created<string>(SharedResourcesKeys.Created);

		}

	}
}
