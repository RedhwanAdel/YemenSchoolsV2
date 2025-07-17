using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Stages.Queries.GetById
{
    public class GetStageByIdQueary : IRequest<Response<GetStageByIdResponse>>
    {
        public GetStageByIdQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
