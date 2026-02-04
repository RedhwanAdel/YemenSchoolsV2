using AutoMapper;
using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsBySchoolId;

namespace YemenSchoolsV1.Application.Features.Students.Queries.GetStudentsByAcademicYearAndSection
{
    public class GetStudentsByAcademicYearAndSectionQueryHandler : IRequestHandler<GetStudentsByAcademicYearAndSectionQuery, IEnumerable<StudentListDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsByAcademicYearAndSectionQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentListDto>> Handle(GetStudentsByAcademicYearAndSectionQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsByAcademicYearAndSectionAsync(request.AcademicYearId, request.SectionId);
            return _mapper.Map<IEnumerable<StudentListDto>>(students);
        }
    }
}
