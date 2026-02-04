using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Grades.Queries
{
    public class GetGradesListQuearyHandler : ResponseHandler, IRequestHandler<GetGradesListQueary, Response<List<GetGradesListResponse>>>
    {
        private readonly IGradeRepository gradeRepository;
        #region faild

        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetGradesListQuearyHandler(IGradeRepository gradeRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.gradeRepository = gradeRepository;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;

        }

        #endregion
        public async Task<Response<List<GetGradesListResponse>>> Handle(GetGradesListQueary request, CancellationToken cancellationToken)
        {
            var resultDomain = await gradeRepository.GetAllAsync();
            var result = mapper.Map<List<GetGradesListResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetGradesListResponse>>();
            }

            return Success(result);
        }

    }
}
