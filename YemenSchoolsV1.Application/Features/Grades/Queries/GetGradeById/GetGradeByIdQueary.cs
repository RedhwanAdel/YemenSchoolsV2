using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.Grades.Queries.GetGradeById
{
    public class GetGradeByIdQueary : IRequest<Response<GetGradeByIdResponse>>
    {
        public GetGradeByIdQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
