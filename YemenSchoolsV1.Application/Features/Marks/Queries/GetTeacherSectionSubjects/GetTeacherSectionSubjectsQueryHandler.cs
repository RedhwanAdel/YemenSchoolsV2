using MediatR;
using YemenSchoolsV1.Application.Contracts.Persistence;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects
{
    public class GetTeacherSectionSubjectsQueryHandler : IRequestHandler<GetTeacherSectionSubjectsQuery, IEnumerable<SectionSubjectDto>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherSectionSubjectsQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<SectionSubjectDto>> Handle(GetTeacherSectionSubjectsQuery request, CancellationToken cancellationToken)
        {
            var sectionSubjects = await _teacherRepository.GetTeacherSectionSubjectsAsync(request.TeacherId);

            var sectionSubjectDtos = sectionSubjects.Select(ss => new SectionSubjectDto
            {
                Id = ss.Id,
                SectionName = ss.Section.Name,
                SubjectName = ss.GradeSubject.Subject.Name,
                SectionId = ss.SectionId,
                GradeName = ss.Section.SchoolGrade.StageGrade.Grade.Name
            }).ToList();

            return sectionSubjectDtos;
        }
    }
}
