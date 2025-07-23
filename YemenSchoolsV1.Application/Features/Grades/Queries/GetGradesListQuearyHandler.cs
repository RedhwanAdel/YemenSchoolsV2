using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Grades.Queries
{
    public class GetGradesListQuearyHandler : ResponseHandler, IRequestHandler<GetGradesListQueary, Response<List<GetGradesListResponse>>>
    {
        private readonly IGradeRepositry gradeRepositry;
        #region faild

        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetGradesListQuearyHandler(IGradeRepositry gradeRepositry, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.gradeRepositry = gradeRepositry;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        #endregion
        public async Task<Response<List<GetGradesListResponse>>> Handle(GetGradesListQueary request, CancellationToken cancellationToken)
        {
            var resultDomain = await gradeRepositry.GetAllAsync();
            var result = mapper.Map<List<GetGradesListResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetGradesListResponse>>();
            }

            return Success(result);
        }

    }
}
