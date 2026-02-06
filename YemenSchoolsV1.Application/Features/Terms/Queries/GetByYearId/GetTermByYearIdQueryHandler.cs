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
        private readonly ITermRepository _termRepository;
        private readonly IMapper _mapper;

        public GetTermByYearIdQueryHandler(ITermRepository termRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _termRepository = termRepository;
            _mapper = mapper;
        }


        public async Task<Response<List<GetTermByYearIdResponse>>> Handle(GetTermByYearIdQuery request, CancellationToken cancellationToken)
        {
            var resultDomain = await _termRepository.GetTermByYearIdAsync(request.Id);

            var result = _mapper.Map<List<GetTermByYearIdResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetTermByYearIdResponse>>();
            }

            return Success(result);
        }


    }
}
