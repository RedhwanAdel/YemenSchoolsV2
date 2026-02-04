using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetByYearId
{
    public class GetTermByYearIdQueryHandler : ResponseHandler, IRequestHandler<GetTermByYearIdQuery, Response<List<GetTermByYearIdResponse>>>
    {
        private readonly ITermRepository termRepository;
        #region faild
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;

        #endregion

        #region ctor
        public GetTermByYearIdQueryHandler(ITermRepository termRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.termRepository = termRepository;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }
        #endregion

        public async Task<Response<List<GetTermByYearIdResponse>>> Handle(GetTermByYearIdQuery request, CancellationToken cancellationToken)
        {
            var resultDomain = await termRepository.GetTermByYearIdAsync(request.Id);

            var result = mapper.Map<List<GetTermByYearIdResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetTermByYearIdResponse>>();
            }

            return Success(result);
        }


    }
}
