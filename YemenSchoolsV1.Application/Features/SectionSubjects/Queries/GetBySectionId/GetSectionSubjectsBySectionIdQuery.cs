using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetBySectionId
{
    public class GetSectionSubjectsBySectionIdQuery : IRequest<Response<List<SectionSubjectInfoDto>>>
    {
        public Guid SectionId { get; set; }
        public GetSectionSubjectsBySectionIdQuery(Guid sectionId) => SectionId = sectionId;
    }
}
