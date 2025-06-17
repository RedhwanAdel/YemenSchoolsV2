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

namespace YemenSchoolsV1.Application.Features.Teachers.Commands.Delete
{
    public class DeleteTeacherCommandHandler : ResponseHandler, IRequestHandler<DeleteTeacherCommand, Response<bool>>
    {
        #region الفيلدز
        private readonly ITeacherService teacherService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region المُنشئ
        public DeleteTeacherCommandHandler(ITeacherService teacherService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            this.teacherService = teacherService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        public async Task<Response<bool>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherDeleted = await teacherService.DeleteTeacherAsync(request.Id);
            if (!teacherDeleted)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
