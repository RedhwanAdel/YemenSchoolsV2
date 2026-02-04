using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetAll
{
    public class GetAllSectionSubjectsQuery : IRequest<Response<List<SectionSubjectInfoDto>>>
    {
    }
}
