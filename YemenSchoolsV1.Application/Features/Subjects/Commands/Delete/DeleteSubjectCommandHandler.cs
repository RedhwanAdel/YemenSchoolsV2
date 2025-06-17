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
using YemenSchoolsV1.Application.Features.Stages.Commands.Delete;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Subjects.Commands.Delete
{
    public class DeleteSubjectCommandHandler : ResponseHandler, IRequestHandler<DeleteSubjectCommand, Response<bool>>
    {
        #region faild
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public DeleteSubjectCommandHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public async Task<Response<bool>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await subjectService.DeleteSubjectAsync(request.Id);
            if (subject is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }

    }
}
