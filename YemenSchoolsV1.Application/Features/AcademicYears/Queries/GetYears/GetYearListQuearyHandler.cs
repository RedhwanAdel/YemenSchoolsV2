using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
    public class GetYearListQuearyHandler : ResponseHandler, IRequestHandler<GetYearListQueary, Response<List<GetYearListResponse>>>
    {
        private readonly IAcademicYearRepository academicYearRepository;
        #region faild

        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        #endregion

        #region ctor
        public GetYearListQuearyHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this.academicYearRepository = academicYearRepository;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
        }

        #endregion
        public async Task<Response<List<GetYearListResponse>>> Handle(GetYearListQueary request, CancellationToken cancellationToken)
        {
            var resultDomain = await academicYearRepository.GetYearsBySchoolIdAsync(request.Id);

            var result = mapper.Map<List<GetYearListResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetYearListResponse>>();
            }

            return Success(result);
        }
    }
}
