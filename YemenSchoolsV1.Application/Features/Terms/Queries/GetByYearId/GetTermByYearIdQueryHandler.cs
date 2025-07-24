using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetByYearId
{
    public class GetTermByYearIdQueryHandler : ResponseHandler, IRequestHandler<GetTermByYearIdQuery, Response<List<GetTermByYearIdResponse>>>
    {
        private readonly ITermRepositry termRepositry;
        #region faild
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public GetTermByYearIdQueryHandler(ITermRepositry termRepositry, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.termRepositry = termRepositry;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        public async Task<Response<List<GetTermByYearIdResponse>>> Handle(GetTermByYearIdQuery request, CancellationToken cancellationToken)
        {
            var resultDomain = await termRepositry.GetTermByYearIdAsync(request.Id);

            var result = mapper.Map<List<GetTermByYearIdResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetTermByYearIdResponse>>();
            }

            return Success(result);
        }


    }
}
