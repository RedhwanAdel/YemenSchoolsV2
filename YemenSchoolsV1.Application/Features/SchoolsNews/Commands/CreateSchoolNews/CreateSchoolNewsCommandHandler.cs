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
using YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews
{
    public class CreateSchoolNewsCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolNewsCommand, Response<CreateSchoolNewsResponse>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ISchoolNewsService schoolNewsService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public CreateSchoolNewsCommandHandler(ISchoolService schoolService, ICityService cityService,ISchoolNewsService schoolNewsService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.schoolNewsService = schoolNewsService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }




        #endregion

        public async Task<Response<CreateSchoolNewsResponse>> Handle(CreateSchoolNewsCommand request, CancellationToken cancellationToken)
        {
            var newsDomain = mapper.Map<SchoolNews>(request);
            newsDomain = await schoolNewsService.CreateSchoolNewsAsync(newsDomain);
            if (newsDomain == null)
            {
                return UnprocessableEntity<CreateSchoolNewsResponse>();
            }

            var newsResponse = mapper.Map<CreateSchoolNewsResponse>(newsDomain);
            return Created(newsResponse);
        }
    }
}
