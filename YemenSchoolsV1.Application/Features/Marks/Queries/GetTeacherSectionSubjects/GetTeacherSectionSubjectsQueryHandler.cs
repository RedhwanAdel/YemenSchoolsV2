using MediatR;
using Microsoft.Extensions.Localization;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Resources;

namespace YemenSchoolsV1.Application.Features.Marks.Queries.GetTeacherSectionSubjects
{
    public class GetTeacherSectionSubjectsQueryHandler : ResponseHandler, IRequestHandler<GetTeacherSectionSubjectsQuery, Response<IEnumerable<SectionSubjectDto>>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public GetTeacherSectionSubjectsQueryHandler(ITeacherRepository teacherRepository, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _teacherRepository = teacherRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<IEnumerable<SectionSubjectDto>>> Handle(GetTeacherSectionSubjectsQuery request, CancellationToken cancellationToken)
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

            return Success<IEnumerable<SectionSubjectDto>>(sectionSubjectDtos);
        }
    }
}
