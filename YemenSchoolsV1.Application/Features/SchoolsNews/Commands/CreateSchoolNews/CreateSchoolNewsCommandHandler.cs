using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Commands.CreateSchoolNews
{
    public class CreateSchoolNewsCommandHandler : ResponseHandler, IRequestHandler<CreateSchoolNewsCommand, Response<CreateSchoolNewsResponse>>
    {
        private readonly ISchoolNewsRepository _schoolNewsRepository;
        private readonly IMapper _mapper;

        public CreateSchoolNewsCommandHandler(ISchoolNewsRepository schoolNewsRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolNewsRepository = schoolNewsRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateSchoolNewsResponse>> Handle(CreateSchoolNewsCommand request, CancellationToken cancellationToken)
        {
            var newsDomain = _mapper.Map<SchoolNews>(request);
            newsDomain = await _schoolNewsRepository.AddAsync(newsDomain);
            if (newsDomain == null)
            {
                return UnprocessableEntity<CreateSchoolNewsResponse>();
            }

            var newsResponse = _mapper.Map<CreateSchoolNewsResponse>(newsDomain);
            return Created(newsResponse);
        }
    }
}
