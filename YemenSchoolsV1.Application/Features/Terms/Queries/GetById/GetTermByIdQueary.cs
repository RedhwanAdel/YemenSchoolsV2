using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetById
{
    public class GetTermByIdQueary : IRequest<Response<GetTermByIdResponse>>
    {
        public GetTermByIdQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
