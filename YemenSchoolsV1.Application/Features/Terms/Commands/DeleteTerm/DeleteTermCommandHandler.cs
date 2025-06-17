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
using YemenSchoolsV1.Application.Features.AcademicYears.Commands.DeleteYear;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommandHandler : ResponseHandler, IRequestHandler<DeleteTermCommand, Response<bool>>
    {
        #region faild
     
        private readonly ITermService termService;

        #endregion

        #region ctor
        public DeleteTermCommandHandler(ITermService termService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.termService = termService;
            
        }

        #endregion
        public async Task<Response<bool>> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
        {
            var term = await termService.DeleteTermAsync(request.Id);
            if (term is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
