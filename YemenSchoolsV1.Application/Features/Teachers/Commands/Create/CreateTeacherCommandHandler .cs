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

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Create
{
    public class CreateTeacherCommandHandler : ResponseHandler, IRequestHandler<CreateTeacherCommand, Response<string>>
    {
        private readonly ITeacherService teacherService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        public CreateTeacherCommandHandler(ITeacherService teacherService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            this.teacherService = teacherService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherDomain = mapper.Map<Teacher>(request);
            teacherDomain = await teacherService.CreateTeacherAsync(teacherDomain);
            if (teacherDomain == null)
            {
                return UnprocessableEntity<string>();
            }

            return Created(SharedResourcesKeys.Created);
        }
    }
}
