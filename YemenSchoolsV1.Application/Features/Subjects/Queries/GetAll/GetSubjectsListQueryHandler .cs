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
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll
{
    public class GetSubjectsListQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectsListResponse>>>
    {
        #region الفيلدز
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region المُنشئ
        public GetSubjectsListQueryHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        #region المعالج
        public async Task<Response<List<GetSubjectsListResponse>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjects = await subjectService.GetAllSubjectsAsync();
            var response = mapper.Map<List<GetSubjectsListResponse>>(subjects);
            return Success(response);
        }
        #endregion
    }

}
