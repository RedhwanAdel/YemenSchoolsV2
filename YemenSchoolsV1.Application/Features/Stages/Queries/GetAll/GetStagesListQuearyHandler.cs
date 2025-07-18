using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetAll
{
    public class GetStagesListQuearyHandler : ResponseHandler, IRequestHandler<GetStagesListQueary, PaginatedResult<GetStagesListResponse>>
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
        public async Task<PaginatedResult<GetStagesListResponse>> Handle(GetStagesListQueary request, CancellationToken cancellationToken)
        {
            var stages = await stageRepositry.GetPagedAsync(predicate: x => x.SchoolId == request.SchoolId,
                paginationQuery: request.Pagination,
                orderBy: x => x.OrderBy(s => s.Name),
                 includes: new List<Expression<Func<Stage, object>>> { s => s.School });



            return PaginatedResult<GetStagesListResponse>.Success(
                mapper.Map<List<GetStagesListResponse>>(stages.Data),
                stages.TotalRecords,
                stages.PageNumber,
                stages.PageSize
                );
        }
    }
}
