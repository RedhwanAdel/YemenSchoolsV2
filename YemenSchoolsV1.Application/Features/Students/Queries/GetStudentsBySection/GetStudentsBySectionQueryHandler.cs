using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySection
{
    public class GetStudentsBySectionQueryHandler : ResponseHandler, IRequestHandler<GetStudentsBySectionQuery, Response<List<StudentListDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentsBySectionQueryHandler(IStudentRepository studentRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<StudentListDto>>> Handle(GetStudentsBySectionQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsBySectionIdAsync(request.SectionId);
            var result = _mapper.Map<List<StudentListDto>>(students);
            return Success(result);
        }
    }
}
