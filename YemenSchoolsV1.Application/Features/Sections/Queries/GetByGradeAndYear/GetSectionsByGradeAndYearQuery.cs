using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetByGradeAndYear
{
    public class GetSectionsByGradeAndYearQuery : IRequest<Response<IEnumerable<SectionByGradeAndYearDto>>>
    {
        public Guid AcademicYearId { get; set; }
        public Guid SchoolGradeId { get; set; }
        public GetSectionsByGradeAndYearQuery(Guid academicYearId, Guid schoolGradeId)
        {
            AcademicYearId = academicYearId;
            SchoolGradeId = schoolGradeId;
        }
    }
}
