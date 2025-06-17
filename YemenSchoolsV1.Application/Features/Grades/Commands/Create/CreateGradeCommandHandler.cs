using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Grades.Commands.Create
{
    public class CreateGradeCommandHandler : ResponseHandler, IRequestHandler<CreateGradeCommand, Response<string>>
    {
        #region faild
        private readonly IGradeService gradeService;


        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateGradeCommandHandler(IGradeService gradeService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.gradeService = gradeService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        #endregion

        public async Task<Response<string>> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var gradeDomain = mapper.Map<Grade>(request);
            gradeDomain = await gradeService.CreateGradeAsync(gradeDomain);
            if (gradeDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);

        }


    }
}
