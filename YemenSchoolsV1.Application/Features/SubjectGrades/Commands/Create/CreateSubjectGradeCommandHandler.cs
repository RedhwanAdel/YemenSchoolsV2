using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SubjectGrades.Commands.Create
{
	public class CreateSubjectGradeCommandHandler : ResponseHandler, IRequestHandler<CreateSubjectGradeCommand, Response<string>>
	{
		private readonly ISubjectGradeRepositry subjectGradeRepositry;
		private readonly ISubjectRepositry subjectRepositry;
		private readonly IGradeRepositry gradeRepositry;
		#region faild
		private readonly IMapper mapper;
		private readonly IStringLocalizer<SharedResources> stringLocalizer;

		#endregion

		#region ctor
		public CreateSubjectGradeCommandHandler(ISubjectGradeRepositry subjectGradeRepositry,
			ISubjectRepositry subjectRepositry,
			IGradeRepositry gradeRepositry,
			IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this.subjectGradeRepositry = subjectGradeRepositry;
			this.subjectRepositry = subjectRepositry;
			this.gradeRepositry = gradeRepositry;
			this.mapper = mapper;
			this.stringLocalizer = stringLocalizer;
		}
		#endregion

		public async Task<Response<string>> Handle(CreateSubjectGradeCommand request, CancellationToken cancellationToken)
		{
			var subjectId = await subjectRepositry.IsExist(request.SubjectId);
			if (!subjectId) return BadRequest<string>(SharedResourcesKeys.NotFound);
			var gradeId = await gradeRepositry.IsExist(request.GradeId);
			if (!gradeId) return BadRequest<string>(SharedResourcesKeys.NotFound);
			var subjectGrade = mapper.Map<SubjectGrade>(request);
			subjectGrade = await subjectGradeRepositry.AddAsync(subjectGrade);
			if (subjectGrade == null)
			{
				return UnprocessableEntity<string>();
			}

			return Created(SharedResourcesKeys.Created);
		}


	}


}
