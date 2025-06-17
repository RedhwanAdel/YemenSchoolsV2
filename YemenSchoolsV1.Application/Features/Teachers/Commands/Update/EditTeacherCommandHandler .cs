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
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Update
{
    public class EditTeacherCommandHandler : ResponseHandler, IRequestHandler<EditTeacherCommand, Response<string>>
    {
        #region الفيلدز
        private readonly ITeacherService teacherService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region المُنشئ
        public EditTeacherCommandHandler(ITeacherService teacherService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            this.teacherService = teacherService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        public async Task<Response<string>> Handle(EditTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return BadRequest<string>();
            }

            var teacherDomain = mapper.Map<Teacher>(request);
            teacherDomain = await teacherService.EditTeacherAsync(request.Id, teacherDomain);

            if (teacherDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Success<string>(SharedResourcesKeys.Update);
        }
    }
}
