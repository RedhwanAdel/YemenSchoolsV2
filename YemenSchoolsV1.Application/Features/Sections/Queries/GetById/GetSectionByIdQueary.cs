using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Sections.Queries.GetById
{
    public class GetSectionByIdQueary : IRequest<Response<GetSectionByIdResponse>>
    {
        public GetSectionByIdQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
