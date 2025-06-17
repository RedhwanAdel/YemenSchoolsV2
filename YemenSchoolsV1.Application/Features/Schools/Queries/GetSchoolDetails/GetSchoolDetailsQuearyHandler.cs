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
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolsPaginated;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails
{
    public class GetSchoolDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolDetailsQuery, Response<GetSchoolDetailsResponse>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetSchoolDetailsQuearyHandler(ISchoolService schoolService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<GetSchoolDetailsResponse>> Handle(GetSchoolDetailsQuery request, CancellationToken cancellationToken)
        {
            var school = await schoolService.GetSchoolDetailsIncludeAsync(request.Id);
            if (school == null)
            {
                return NotFound<GetSchoolDetailsResponse>();
            }
            var result = mapper.Map<GetSchoolDetailsResponse>(school);
            return Success(result);
        }

        #endregion

    }
}
