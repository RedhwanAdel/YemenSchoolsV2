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
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails
{
    public class GetSchoolNewsDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolNewsDetailsQueary, Response<GetSchoolNewsDetailsRseponse>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ISchoolNewsService schoolNewsService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetSchoolNewsDetailsQuearyHandler(ISchoolService schoolService, ICityService cityService, ISchoolNewsService schoolNewsService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.schoolNewsService = schoolNewsService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }
        #endregion
        public async Task<Response<GetSchoolNewsDetailsRseponse>> Handle(GetSchoolNewsDetailsQueary request, CancellationToken cancellationToken)
        {
            var news = await schoolNewsService.GetSchoolNewsDetailsAsync(request.Id);
            if (news == null)
            {
                return NotFound<GetSchoolNewsDetailsRseponse>();
            }
            var result = mapper.Map<GetSchoolNewsDetailsRseponse>(news);
            return Success(result);
        }
    }
}

