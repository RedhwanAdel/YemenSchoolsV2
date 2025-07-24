using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Terms.Queries.GetByYearId
{
    public class GetTermByYearIdQuery : IRequest<Response<List<GetTermByYearIdResponse>>>
    {
        public GetTermByYearIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
