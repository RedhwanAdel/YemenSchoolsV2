using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsList
{
    public class GetSchoolNewsListQuereyHandler : ResponseHandler, IRequestHandler<GetSchoolNewsListQuerey, Response<List<GetSchoolNewsListResponse>>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolNewsRepository _schoolNewsRepository;
        private readonly IMapper _mapper;

        public GetSchoolNewsListQuereyHandler(ISchoolRepository schoolRepository, ISchoolNewsRepository schoolNewsRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
            _schoolNewsRepository = schoolNewsRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetSchoolNewsListResponse>>> Handle(GetSchoolNewsListQuerey request, CancellationToken cancellationToken)
        {
            var school = await _schoolRepository.GetByIdAsync(request.SchoolId);
            if (school == null)
            {
                return BadRequest<List<GetSchoolNewsListResponse>>();
            }

            var news = await _schoolNewsRepository.GetSchoolNewsBySchoolIdAsync(request.SchoolId);
            if (news == null)
            {
                return NotFound<List<GetSchoolNewsListResponse>>();
            }
            var result = _mapper.Map<List<GetSchoolNewsListResponse>>(news);
            return Success(result);
        }
    }
}
