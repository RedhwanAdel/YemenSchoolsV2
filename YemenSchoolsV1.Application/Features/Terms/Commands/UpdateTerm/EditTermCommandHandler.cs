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
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.UpdateYear;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.UpdateTerm
{
    public class EditTermCommandHandler : ResponseHandler, IRequestHandler<EditTermCommand, Response<string>>
    {
        #region faild
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly ITermService termService;

        #endregion

        #region ctor
        public EditTermCommandHandler(ITermService termService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.termService = termService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public async Task<Response<string>> Handle(EditTermCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var termDomain = mapper.Map<Term>(request);
            termDomain = await termService.EditTermAsync(request.Id, termDomain);
            if (termDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
