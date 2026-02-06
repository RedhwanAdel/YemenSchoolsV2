using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId
{
    public class GetStudentsBySchoolIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentsBySchoolIdQuery, Response<IEnumerable<StudentListDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetStudentsBySchoolIdQueryHandler(IStudentRepository studentRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<IEnumerable<StudentListDto>>> Handle(GetStudentsBySchoolIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsBySchoolIdAsync(request.SchoolId);
            var result = _mapper.Map<IEnumerable<StudentListDto>>(students);
            return Success(result);
        }
    }
}
