using AutoMapper;
using FinalProject.Application.Bases;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Subjects.Queries.GetById
{
    public class GetSubjectDetailsQueryHandler : ResponseHandler, IRequestHandler<GetSubjectDetailsQuery, Response<GetSubjectDetailsResponse>>
    {
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly ISubjectService subjectService;
        private readonly IMapper mapper;

        public GetSubjectDetailsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, ISubjectService subjectService, IMapper mapper)
            : base(stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
            this.subjectService = subjectService;
            this.mapper = mapper;
        }

        public async Task<Response<GetSubjectDetailsResponse>> Handle(GetSubjectDetailsQuery request, CancellationToken cancellationToken)
        {
            var subject = await subjectService.GetSubjectDetailsAsync(request.Id);
            if (subject == null)
            {
                return NotFound<GetSubjectDetailsResponse>();
            }
            var result = mapper.Map<GetSubjectDetailsResponse>(subject);
            return Success(result);
        }
    }

}
