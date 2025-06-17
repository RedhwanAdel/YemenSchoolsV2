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

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Update
{
    public class EditSubjectCommandHandler : ResponseHandler, IRequestHandler<EditSubjectCommand, Response<string>>
    {
        #region faild
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public EditSubjectCommandHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public  async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var subjetDomain = mapper.Map<Subject>(request);
            subjetDomain = await subjectService.EditSubjectAsync(request.Id, subjetDomain);
            if (subjetDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }

    }
}
