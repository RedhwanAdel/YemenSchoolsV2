using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails
{
    public class GetSchoolDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolDetailsQuery, Response<GetSchoolDetailsResponse>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public GetSchoolDetailsQuearyHandler(ISchoolRepository schoolRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetSchoolDetailsResponse>> Handle(GetSchoolDetailsQuery request, CancellationToken cancellationToken)
        {
            var school = await _schoolRepository.GetSchoolDetailsInculdeAsync(request.Id);
            if (school == null)
            {
                return NotFound<GetSchoolDetailsResponse>();
            }
            var result = _mapper.Map<GetSchoolDetailsResponse>(school);
            return Success(result);
        }
    }
}
