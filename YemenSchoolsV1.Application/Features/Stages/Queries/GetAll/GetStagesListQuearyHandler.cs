using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
    public class GetStagesListQuearyHandler : ResponseHandler, IRequestHandler<GetStagesListQueary, Response<List<GetStagesListResponse>>>
    {
        #region Fields
        private readonly IStageRepositry stageRepositry;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetStagesListQuearyHandler(IStageRepositry stageRepositry,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.stageRepositry = stageRepositry;
            _localizer = localizer;
            this.mapper = mapper;
        }

        #endregion
        public async Task<Response<List<GetStagesListResponse>>> Handle(GetStagesListQueary request, CancellationToken cancellationToken)
        {
            var stagesDomain = await stageRepositry.GetAllAsync();
            var stages = mapper.Map<List<GetStagesListResponse>>(stagesDomain);

            if (stages == null)
            {
                return NotFound<List<GetStagesListResponse>>();
            }
            return Success(stages);
        }
    }
}
