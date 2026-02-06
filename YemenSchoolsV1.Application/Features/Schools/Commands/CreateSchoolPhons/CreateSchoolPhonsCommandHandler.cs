using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.CreateSchoolPhons
{
    public class CreateSchoolPhonsCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolPhonsCommand, Response<string>>
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public CreateSchoolPhonsCommandHandler(ISchoolRepository schoolRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSchoolPhonsCommand request, CancellationToken cancellationToken)
        {
            var school = await _schoolRepository.GetByIdAsync(request.SchoolId);
            if (school == null) return BadRequest<string>();

            List<SchoolPhone> phones = new List<SchoolPhone>();
            foreach (var phone in request.PhoneNumber)
            {
                phones.Add(new SchoolPhone()
                {
                    SchoolId = request.SchoolId,
                    PhoneNumber = phone
                });
            }

            await _schoolRepository.CreateSchoolPhonesRangAsync(phones);
           
            return Created(SharedResourcesKeys.Created);
        }
    }
}
