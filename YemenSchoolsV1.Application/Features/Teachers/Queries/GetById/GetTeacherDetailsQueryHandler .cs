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

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetById
{
    public class GetTeacherDetailsQueryHandler : ResponseHandler, IRequestHandler<GetTeacherDetailsQuery, Response<GetTeacherDetailsResponse>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public GetTeacherDetailsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, ITeacherRepository teacherRepository, IMapper mapper)
            : base(stringLocalizer)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetTeacherDetailsResponse>> Handle(GetTeacherDetailsQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacher == null)
            {
                return NotFound<GetTeacherDetailsResponse>();
            }
            var result = _mapper.Map<GetTeacherDetailsResponse>(teacher);
            return Success(result);
        }
    }

}
