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
using YemenSchoolsV1.Application.Features.Schools.Commands.DeleteSchool;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.DeleteSchoolNews
{
    public class DeleteSchoolNewsCommandHandler : ResponseHandler, IRequestHandler<DeleteSchoolNewsCommand, Response<bool>>
    {
        #region faild

        private readonly ISchoolService schoolService;
        private readonly ISchoolNewsService schoolNewsService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public DeleteSchoolNewsCommandHandler(ISchoolService schoolService, ICityService cityService, ISchoolNewsService schoolNewsService, IRegionService regionService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.schoolService = schoolService;
            this.schoolNewsService = schoolNewsService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        public async Task<Response<bool>> Handle(DeleteSchoolNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await schoolNewsService.DeleteSchoolNewsAsync(request.Id);
            if (news is false)
            {
                return UnprocessableEntity<bool>();
            }
            return Deleted<bool>();
        }

        #endregion
    }
}
