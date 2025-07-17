using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYearById
{
    public class GetYearByIdQueary : IRequest<Response<GetYearByIdResponse>>
    {
        public GetYearByIdQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
