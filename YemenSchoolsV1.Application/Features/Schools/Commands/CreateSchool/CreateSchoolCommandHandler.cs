using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolCommand, Response<CreateSchoolResponse>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public CreateSchoolCommandHandler(ISchoolRepository schoolRepository, ICityRepository cityRepository, IRegionRepository regionRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
            _cityRepository = cityRepository;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateSchoolResponse>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetByIdAsync(request.RegionId);
            if (region == null) return BadRequest<CreateSchoolResponse>();
            var city = await _cityRepository.GetByIdAsync(request.CityId);
            if (city == null) return BadRequest<CreateSchoolResponse>();

            var schoolDomain = _mapper.Map<School>(request);
            schoolDomain = await _schoolRepository.AddAsync(schoolDomain);
            if (schoolDomain == null)
            {
                return UnprocessableEntity<CreateSchoolResponse>();
            }

            var schoolResponse = _mapper.Map<CreateSchoolResponse>(schoolDomain);
            return Created(schoolResponse);
        }
    }
}
