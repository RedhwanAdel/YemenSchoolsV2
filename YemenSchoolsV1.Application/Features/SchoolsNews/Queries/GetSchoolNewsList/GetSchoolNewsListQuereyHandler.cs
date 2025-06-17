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
using YemenSchoolsV1.Application.Features.Regions.Queries.GetRegionsByCityId;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList
{
    public class GetSchoolNewsListQuereyHandler : ResponseHandler, IRequestHandler<GetSchoolNewsListQuerey, Response<List<GetSchoolNewsListResponse>>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ISchoolNewsService schoolNewsService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetSchoolNewsListQuereyHandler(ISchoolService schoolService, ICityService cityService, ISchoolNewsService schoolNewsService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.schoolNewsService = schoolNewsService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<List<GetSchoolNewsListResponse>>> Handle(GetSchoolNewsListQuerey request, CancellationToken cancellationToken)
        {
            var school = await schoolService.GetSchoolDetailsAsync(request.SchoolId);
            if (school == null)
            {
                return BadRequest<List<GetSchoolNewsListResponse>>();
            }

            var news = await schoolNewsService.GetSchoolNewsDetailsBySchoolIdAsync(request.SchoolId);
            if (news == null)
            {
                return NotFound<List<GetSchoolNewsListResponse>>();
            }
            var result = mapper.Map<List<GetSchoolNewsListResponse>>(news);
            return Success(result);
        }




        #endregion
    }
}
