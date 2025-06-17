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

namespace YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler : ResponseHandler, IRequestHandler<DeleteCityCommand, Response<bool>>
    {
        private readonly ICityService cityService;

        public DeleteCityCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,ICityService cityService) : base(stringLocalizer)
        {
            this.cityService = cityService;
        }

        public async Task<Response<bool>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city =await cityService.DeleteCityAsync(request.Id);
            if (city is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }
    }
}
