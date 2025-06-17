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

namespace YemenSchoolsV1.Application.Features.Sections.Commands.Delete
{
    public class DeleteSectionCommandHandler : ResponseHandler, IRequestHandler<DeleteSectionCommand, Response<bool>>
    {
        #region faild
        private readonly ISectionService sectionService;


        #endregion

        #region ctor
        public DeleteSectionCommandHandler(ISectionService sectionService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.sectionService = sectionService;
        }

        #endregion
        public async Task<Response<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await sectionService.DeleteSectionAsync(request.Id);
            if (section is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
