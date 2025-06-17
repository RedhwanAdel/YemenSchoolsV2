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
using YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Create
{
    public class CreateStageCommandHandler : ResponseHandler, IRequestHandler<CreateStageCommand, Response<string>>
    {
        #region faild
        private readonly IStageService stageService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public CreateStageCommandHandler(IStageService stageService , IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.stageService = stageService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public async Task<Response<string>> Handle(CreateStageCommand request, CancellationToken cancellationToken)
        {
            var stageDomain = mapper.Map<Stage>(request);
            stageDomain = await stageService.CreateStageAsync(stageDomain);
            if (stageDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
