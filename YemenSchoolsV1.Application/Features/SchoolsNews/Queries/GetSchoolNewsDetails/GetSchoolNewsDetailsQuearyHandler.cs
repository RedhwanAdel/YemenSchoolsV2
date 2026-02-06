using AutoMapper;
using YemenSchoolsV1.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Resources;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails
{
    public class GetSchoolNewsDetailsQuearyHandler : ResponseHandler, IRequestHandler<GetSchoolNewsDetailsQueary, Response<GetSchoolNewsDetailsRseponse>>
    {
        private readonly ISchoolNewsRepository _schoolNewsRepository;
        private readonly IMapper _mapper;

        public GetSchoolNewsDetailsQuearyHandler(ISchoolNewsRepository schoolNewsRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _schoolNewsRepository = schoolNewsRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetSchoolNewsDetailsRseponse>> Handle(GetSchoolNewsDetailsQueary request, CancellationToken cancellationToken)
        {
            var news = await _schoolNewsRepository.GetByIdAsync(request.Id);
            if (news == null)
            {
                return NotFound<GetSchoolNewsDetailsRseponse>();
            }
            var result = _mapper.Map<GetSchoolNewsDetailsRseponse>(news);
            return Success(result);
        }
    }
}
