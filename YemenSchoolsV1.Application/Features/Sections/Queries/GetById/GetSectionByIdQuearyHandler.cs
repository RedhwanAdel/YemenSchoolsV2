using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetById
{
    public class GetSectionByIdQuearyHandler : ResponseHandler, IRequestHandler<GetSectionByIdQueary, Response<GetSectionByIdResponse>>
    {
        #region Fields
        private readonly ISectionRepositry sectionRepositry;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public GetSectionByIdQuearyHandler(ISectionRepositry sectionRepositry,
                                   IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            this.sectionRepositry = sectionRepositry;
            _localizer = localizer;
            this.mapper = mapper;
        }

        #endregion
        public async Task<Response<GetSectionByIdResponse>> Handle(GetSectionByIdQueary request, CancellationToken cancellationToken)
        {
            var section = await sectionRepositry.GetSectioneByIdIncludeAsync(request.Id);
            if (section == null)
            {
                return NotFound<GetSectionByIdResponse>();
            }
            var result = mapper.Map<GetSectionByIdResponse>(section);
            return Success(result);
        }

    }
}
