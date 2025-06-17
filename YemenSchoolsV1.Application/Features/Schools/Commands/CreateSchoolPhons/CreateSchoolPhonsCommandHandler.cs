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
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons
{
    public class CreateSchoolPhonsCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolPhonsCommand, Response<string>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateSchoolPhonsCommandHandler(ISchoolService schoolService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<string>> Handle(CreateSchoolPhonsCommand request, CancellationToken cancellationToken)
        {
           
            var school = await schoolService.GetSchoolDetailsAsync(request.SchoolId);
            if (school == null) return BadRequest<string>();
            List<SchoolPhone> phones = new List<SchoolPhone>();
            foreach (var phone in request.PhoneNumber)
            {
                phones.Add(new SchoolPhone()
                {
                    SchoolId = request.SchoolId,
                    PhoneNumber = phone
                });
            }

            await schoolService.CreateSchoolPhonsRangAsync(phones);
           
            return Created(SharedResourcesKeys.Created);
        }
        #endregion

    }
}
