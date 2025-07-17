using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetById
{
    public class GetTermByIdQuearyHandler : ResponseHandler, IRequestHandler<GetTermByIdQueary, Response<GetTermByIdResponse>>
    {
        #region Fields
        private readonly ITermRepositry termRepositry;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetTermByIdQuearyHandler(ITermRepositry termRepositry,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.termRepositry = termRepositry;
            _localizer = localizer;
            this.mapper = mapper;
        }
        #endregion

        public async Task<Response<GetTermByIdResponse>> Handle(GetTermByIdQueary request, CancellationToken cancellationToken)
        {
            var term = await termRepositry.GetTermByIdIncludeAsync(request.Id);
            if (term == null)
            {
                return NotFound<GetTermByIdResponse>();
            }
            var result = mapper.Map<GetTermByIdResponse>(term);
            return Success(result);
        }

    }
}
