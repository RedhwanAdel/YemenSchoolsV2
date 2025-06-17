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
using YemenSchoolsV1.Application.Features.Stages.Commands.Create;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Create
{
    public class CreateSectionCommandHandler : ResponseHandler, IRequestHandler<CreateSectionCommand, Response<string>>
    {

        #region faild
        private readonly ISectionService sectionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public CreateSectionCommandHandler(ISectionService sectionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.sectionService = sectionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var sectionDomain = mapper.Map<Section>(request);
            sectionDomain = await sectionService.CreateSectionAsync(sectionDomain);
            if (sectionDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }

        #endregion
    }
}
