using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Dto;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetById
{
    public class GetSectionByIdQuery : IRequest<Response<SectionDto>>
    {
        public Guid Id { get; set; }
        public GetSectionByIdQuery(Guid id) => Id = id;
    }
}
