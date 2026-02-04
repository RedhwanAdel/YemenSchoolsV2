using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.SectionSubjects.Queries.GetById
{
    public class GetSectionSubjectByIdQuery : IRequest<Response<SectionSubjectInfoDto>>
    {
        public Guid Id { get; set; }
        public GetSectionSubjectByIdQuery(Guid id) => Id = id;
    }
}
