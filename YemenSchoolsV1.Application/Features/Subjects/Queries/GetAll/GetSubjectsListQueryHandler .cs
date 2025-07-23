using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetAll
{
    public class GetSubjectsListQueryHandler : ResponseHandler, IRequestHandler<GetSubjectsListQuery, Response<List<GetSubjectsListResponse>>>
    {
        #region 
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region 
        public GetSubjectsListQueryHandler(ISubjectService subjectService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
            : base(stringLocalizer)
        {
            this.subjectService = subjectService;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        #region 
        public async Task<Response<List<GetSubjectsListResponse>>> Handle(GetSubjectsListQuery request, CancellationToken cancellationToken)
        {
            var subjects = await subjectService.GetAllSubjectsAsync();
            var response = mapper.Map<List<GetSubjectsListResponse>>(subjects);
            return Success(response);
        }
        #endregion
    }

}
