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
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool
{
    internal class DeleteSchoolCommandHandler : ResponseHandler, IRequestHandler<DeleteSchoolCommand, Response<bool>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public DeleteSchoolCommandHandler(ISchoolService schoolService, ICityService cityService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.cityService = cityService;
            this.regionService = regionService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<bool>> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
        {
            var school = await schoolService.DeleteSchoolAsync(request.Id);
            if (school is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }


        #endregion

    }
}
