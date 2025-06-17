using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear
{
    public class EditYearCommandHandler : ResponseHandler, IRequestHandler<EditYearCommand, Response<string>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly IAcademicYearService academicYearService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion
        public EditYearCommandHandler(ISchoolService schoolService, IAcademicYearService academicYearService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.academicYearService = academicYearService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }
        public async Task<Response<string>> Handle(EditYearCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var yearDomain = mapper.Map<AcademicYear>(request);
            yearDomain = await academicYearService.EditYearAsync(request.Id, yearDomain);
            if (yearDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);

        }
    }
}
