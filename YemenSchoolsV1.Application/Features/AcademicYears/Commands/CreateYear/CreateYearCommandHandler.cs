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
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.CreateYear
{
    public class CreateYearCommandHandler : ResponseHandler, IRequestHandler<CreateYearCommand, Response<string>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly IAcademicYearService academicYearService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateYearCommandHandler(ISchoolService schoolService,IAcademicYearService academicYearService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.academicYearService = academicYearService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<string>> Handle(CreateYearCommand request, CancellationToken cancellationToken)
        {
            var yearDomain = mapper.Map<AcademicYear>(request);
            yearDomain = await academicYearService.CreateYearAsync(yearDomain);
            if (yearDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);

        }


        #endregion
    }
}
