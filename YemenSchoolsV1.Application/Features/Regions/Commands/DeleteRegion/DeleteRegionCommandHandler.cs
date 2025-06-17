using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommandHandler : ResponseHandler, IRequestHandler<DeleteRegionCommand, Response<bool>>
    {
        private readonly IRegionService regionService;

        public DeleteRegionCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IRegionService regionService) : base(stringLocalizer)
        { 
            this.regionService = regionService;
        }

        public async Task<Response<bool>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await regionService.DeleteRegionAsync(request.Id);
            if (region is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
