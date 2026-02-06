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
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IMapper _mapper;

        public GetYearListQuearyHandler(IAcademicYearRepository academicYearRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _academicYearRepository = academicYearRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetYearListResponse>>> Handle(GetYearListQueary request, CancellationToken cancellationToken)
        {
            var resultDomain = await _academicYearRepository.GetYearsBySchoolIdAsync(request.Id);

            var result = _mapper.Map<List<GetYearListResponse>>(resultDomain);

            if (result == null)
            {
                return NotFound<List<GetYearListResponse>>();
            }

            return Success(result);
        }
    }
}
