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
using YemenSchoolsV1.Application.Features.Stages.Commands.Update;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Update
{
    public class EditSectionCommandHandler : ResponseHandler, IRequestHandler<EditSectionCommand, Response<string>>
    {
        #region faild
        private readonly ISectionService sectionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public EditSectionCommandHandler(ISectionService sectionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.sectionService = sectionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        public async Task<Response<string>> Handle(EditSectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var sectionDomain = mapper.Map<Section>(request);
            sectionDomain = await sectionService.EditSectionAsync(request.Id, sectionDomain);
            if (sectionDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }

    }
}
