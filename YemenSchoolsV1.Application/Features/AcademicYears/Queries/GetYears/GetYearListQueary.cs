using FinalProject.Application.Bases;
using MediatR;

namespace YemenSchoolsV1.Application.Features.AcademicYears.Queries.GetYears
{
    public class GetYearListQueary : IRequest<Response<List<GetYearListResponse>>>
    {
        public GetYearListQueary(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
