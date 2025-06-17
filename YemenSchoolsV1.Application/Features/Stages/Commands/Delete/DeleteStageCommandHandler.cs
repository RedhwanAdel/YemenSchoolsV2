using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Stages.Commands.Delete
{
    public class DeleteStageCommandHandler : ResponseHandler, IRequestHandler<DeleteStageCommand, Response<bool>>
    {
        #region faild
        private readonly IStageService stageService;


        #endregion

        #region ctor
        public DeleteStageCommandHandler(IStageService stageService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.stageService = stageService;
        }

        #endregion
        public async Task<Response<bool>> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
        {
            var stage = await stageService.DeleteStageAsync(request.Id);
            if (stage is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }

    }
}
