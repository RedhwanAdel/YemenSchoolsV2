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

namespace YemenSchoolsV1.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommandHandler : ResponseHandler, IRequestHandler<CreateTermCommand, Response<string>>
    {
        #region faild
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly ITermService termService;

        #endregion

        #region ctor
        public CreateTermCommandHandler(ITermService termService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.termService = termService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion

        public async Task<Response<string>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
        {
            var termDomain = mapper.Map<Term>(request);
            termDomain = await termService.CreateTermAsync(termDomain);
            if (termDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
