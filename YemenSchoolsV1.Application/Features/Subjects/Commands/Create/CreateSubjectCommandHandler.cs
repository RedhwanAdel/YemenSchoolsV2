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

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectCommandHandler : ResponseHandler, IRequestHandler<CreateSubjectCommand, Response<string>>
    {
        #region faild
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public CreateSubjectCommandHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public async Task<Response<string>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectDomain = mapper.Map<Subject>(request);
            subjectDomain = await subjectService.CreateSubjectAsync(subjectDomain);
            if (subjectDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }

    }
}
