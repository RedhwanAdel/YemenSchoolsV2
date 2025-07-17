using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetById
{
    public class GetStageByIdQuearyHandler : ResponseHandler, IRequestHandler<GetStageByIdQueary, Response<GetStageByIdResponse>>
    {
        #region Fields
        private readonly IStageRepositry stageRepositry;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetStageByIdQuearyHandler(IStageRepositry stageRepositry,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.stageRepositry = stageRepositry;
            _localizer = localizer;
            this.mapper = mapper;
        }
        #endregion

        public async Task<Response<GetStageByIdResponse>> Handle(GetStageByIdQueary request, CancellationToken cancellationToken)
        {
            var stage = await stageRepositry.GetStageByIdIncludeAsync(request.Id);
            if (stage == null)
            {
                return NotFound<GetStageByIdResponse>();
            }
            var result = mapper.Map<GetStageByIdResponse>(stage);
            return Success(result);
        }

    }
}
