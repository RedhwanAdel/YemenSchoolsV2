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

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetById
{
    public class GetTeacherDetailsQueryHandler : ResponseHandler, IRequestHandler<GetTeacherDetailsQuery, Response<GetTeacherDetailsResponse>>
    {
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly ITeacherService teacherService;
        private readonly IMapper mapper;

        public GetTeacherDetailsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, ITeacherService teacherService, IMapper mapper)
            : base(stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
            this.teacherService = teacherService;
            this.mapper = mapper;
        }

        public async Task<Response<GetTeacherDetailsResponse>> Handle(GetTeacherDetailsQuery request, CancellationToken cancellationToken)
        {
            var teacher = await teacherService.GetTeacherDetailsAsync(request.Id);
            if (teacher == null)
            {
                return NotFound<GetTeacherDetailsResponse>();
            }
            var result = mapper.Map<GetTeacherDetailsResponse>(teacher);
            return Success(result);
        }
    }

}
