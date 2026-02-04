using AutoMapper;
using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId
{
    public class GetStudentsBySchoolIdQueryHandler : IRequestHandler<GetStudentsBySchoolIdQuery, IEnumerable<StudentListDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsBySchoolIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentListDto>> Handle(GetStudentsBySchoolIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsBySchoolIdAsync(request.SchoolId);
            return _mapper.Map<IEnumerable<StudentListDto>>(students);
        }
    }
}
